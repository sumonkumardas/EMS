using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ClassTestMessage;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class DisplayAddClassTestPageUseCase : IDisplayAddClassTestPageUseCase
  {
    private readonly IRepository _repository;

    public DisplayAddClassTestPageUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public DisplayAddClassTestPageResponseMessage DisplayAddClassTestPage(
      DisplayAddClassTestPageRequestMessage requestMessage)
    {
      var includes = new[] {nameof(Section.Session), nameof(Section.Class)};
      var section = _repository.GetByIdWithInclude<Section>(requestMessage.SectionId, includes);

      var examConfigurations = _repository.Filter<ExamConfiguration, ExamTerm, Class, Subject>(
        x => x.Term.Session.Id == section.Session.Id &&
             x.Class.Id == section.Class.Id && x.Term.Session.EndDate.Ticks>=DateTime.Now.Ticks,
        x => x.Term,
        x => x.Class,
        x => x.Subject).AsEnumerable();
      var terms = ToResponseTerms(examConfigurations);
      return new DisplayAddClassTestPageResponseMessage
      {
        ClassName = section.Class.ClassName,
        SectionId = requestMessage.SectionId,
        Terms = terms
      };
    }

    private class SubjectData
    {
      public long ExamConfigurationId { get; set; }
      public string SubjectName { get; set; }
    }

    private ICollection<DisplayAddClassTestPageResponseMessage.Term> ToResponseTerms(
      IEnumerable<ExamConfiguration> examConfigurations)
    {
      var responseTerms = new List<DisplayAddClassTestPageResponseMessage.Term>();
      var termMap = new Dictionary<long, IDictionary<string, ICollection<SubjectData>>>();
      var termNameMap = new Dictionary<long, string>();

      foreach (var examConfiguration in examConfigurations)
      {
        var examTerm = examConfiguration.Term;
        var dbClass = examConfiguration.Class;
        var subject = examConfiguration.Subject;
        var groupName = _repository
          .Table<ClassSubject>(new[] {nameof(ClassSubject.Class), nameof(ClassSubject.Subject)})
          .FirstOrDefault(x => x.Class.Id == dbClass.Id && x.Subject.Id == subject.Id).Group.ToString();
        var subjectData = ToSubjectData(subject, examConfiguration);
        //var group = ToMessageEnum(classSubject.Group);
        if (termMap.ContainsKey(examTerm.Id))
        {
          var termData = termMap[examTerm.Id];

          if (termData.ContainsKey(dbClass.ClassName))
          {
            var subjectIdList = termData[dbClass.ClassName];
            subjectIdList.Add(subjectData);
          }
          else
          {
            termData.Add(dbClass.ClassName, new List<SubjectData> {subjectData});
          }
        }
        else
        {
          var groupData = new Dictionary<string, ICollection<SubjectData>>
          {
            {groupName, new List<SubjectData> {subjectData}}
          };
          termMap[examTerm.Id] = groupData;
          termNameMap[examTerm.Id] = examTerm.TermName;
        }
      }

      foreach (var termId in termMap.Keys)
      {
        var groups = new List<DisplayAddClassTestPageResponseMessage.Group>();
        foreach (var group in termMap[termId].Keys)
        {
          var subjects = new List<DisplayAddClassTestPageResponseMessage.Subject>();
          foreach (var subjectData in termMap[termId][group])
          {
            subjects.Add(new DisplayAddClassTestPageResponseMessage.Subject
            {
              ExamConfigurationId = subjectData.ExamConfigurationId,
              SubjectName = subjectData.SubjectName
            });
          }

          groups.Add(new DisplayAddClassTestPageResponseMessage.Group
          {
            Subjects = subjects,
            GroupEnum = group//_repository.Table<ClassSubject>(new []{nameof(ClassSubject.Class),nameof(ClassSubject.Subject)}).FirstOrDefault(x=>x.Class.Id == )
          });
        }

        responseTerms.Add(new DisplayAddClassTestPageResponseMessage.Term
        {
          Groups = groups,
          TermId = termId,
          TermName = termNameMap[termId]
        });
      }

      return responseTerms;
    }

    private static SubjectData ToSubjectData(Subject subject, ExamConfiguration examConfiguration)
    {
      return new SubjectData
      {
        ExamConfigurationId = examConfiguration.Id,
        SubjectName = subject.SubjectName
      };
    }

    private static GroupEnum ToMessageEnum(Domain.Enums.GroupEnum group)
    {
      switch (group)
      {
        case Domain.Enums.GroupEnum.NoGroup:
          return GroupEnum.NoGroup;
        case Domain.Enums.GroupEnum.Arts:
          return GroupEnum.Arts;
        case Domain.Enums.GroupEnum.Commerce:
          return GroupEnum.Commerce;
        case Domain.Enums.GroupEnum.Science:
          return GroupEnum.Science;
        default:
          throw new ArgumentOutOfRangeException(nameof(group), group, null);
      }
    }
  }
}