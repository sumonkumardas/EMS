using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Marks
{
  public class FilterTermTestAddMarkFieldsUseCase : IFilterTermTestAddMarkFieldsUseCase
  {
    private readonly IRepository _repository;

    public FilterTermTestAddMarkFieldsUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public FilterTermTestAddMarkFieldsResponseMessage.TermTestAddMarkObjectsCollection GetFilteredObjects(FilterTermTestAddMarkFieldsRequestMessage message)
    {
      var termTests = _repository.Table<TermTest>()
        .Include(nameof(ExamConfiguration))
        .Where(t => t.ExamConfiguration.Term.Id == message.TermId &&
                    t.ExamConfiguration.Term.Session.BranchMedium.Id == message.BranchMediumId);
      var classes = termTests.Select(t => t.ExamConfiguration).Select(t => t.Class).ToList();
      var requestClasses = ToRequestClasses(classes);
      
      var groups = new List<GroupEnum>();
      if (message.ClassId > 0)
      {
        groups = _repository.Table<ClassSubject>()
          .Where(e => e.Class.Id == message.ClassId)
          .Select(t => t.Group)
          .ToList();
      }
      var requestGroups = ToRequestGroups(groups);
      
      var subjects = new List<Subject>();
      if (message.ClassId > 0)
      {
        var examConfigSubjects = termTests.Select(t => t.ExamConfiguration)
          .Where(e => e.Class.Id == message.ClassId)
          .Select(t => t.Subject).ToList();
        
        var classSubjects = _repository.Table<ClassSubject>()
          .Where(x => x.Class.Id == message.ClassId &&
                      x.Group == (GroupEnum) message.Group)
          .Select(x => x.Subject)
          .ToList();
        
        subjects = examConfigSubjects.Intersect(classSubjects).ToList();
      }
      var requestSubjects = ToRequestSubjects(subjects);

      var examTypes = new List<ExamTypeEnum>();
      if (message.ClassId > 0 && message.SubjectId > 0)
      {
        var examConfigurations = termTests.Select(x => x.ExamConfiguration)
          .Where(x => x.Class.Id == message.ClassId && 
                       x.Subject.Id == message.SubjectId)
          .ToList();
        var termTestsList = termTests.ToList();
        foreach (var termTest in termTestsList)
        {
          foreach (var examConfiguration in examConfigurations)
          {
            if (termTest.ExamConfiguration.Id == examConfiguration.Id)
            {
              examTypes.Add(termTest.ExamType);
            }
          }
        }
      }
      var requestExamTypes = ToRequestExamTypes(examTypes);

      var requestSections = new Collection<FilterTermTestAddMarkFieldsResponseMessage.Section>();
      if (message.ClassId > 0)
      {
        var sections = _repository.Table<Section>()
          .Include(nameof(Class))
          .Where(s => s.Group == (GroupEnum) message.Group &&
                      s.Class.Id == message.ClassId &&
                      s.BranchMedium.Id == message.BranchMediumId)
          .ToList();
        foreach (var section in sections)
        {
          if (section != null)
          {
            requestSections.Add(new FilterTermTestAddMarkFieldsResponseMessage.Section
            {
              SectionId = section.Id,
              SectionName = section.SectionName
            });
          }
        }
      }
      return new FilterTermTestAddMarkFieldsResponseMessage.TermTestAddMarkObjectsCollection
      {
        Classes = requestClasses,
        Subjects = requestSubjects,
        Sections = requestSections,
        Groups = requestGroups,
        ExamTypes = requestExamTypes
      };
    }

    private IEnumerable<FilterTermTestAddMarkFieldsResponseMessage.ExamType> ToRequestExamTypes(List<ExamTypeEnum> examTypes)
    {
      var requestExamTypes = new Collection<FilterTermTestAddMarkFieldsResponseMessage.ExamType>();
      foreach (var examType in examTypes)
      {
        requestExamTypes.Add(new FilterTermTestAddMarkFieldsResponseMessage.ExamType
        {
          ExamTypeId = (int)examType,
          ExamTypeName = examType.ToString("G")
        });
      }

      return requestExamTypes.GroupBy(x => x.ExamTypeId).Select(y => y.First());
    }

    private IEnumerable<FilterTermTestAddMarkFieldsResponseMessage.Group> ToRequestGroups(List<GroupEnum> groups)
    {
      var requestGroups = new Collection<FilterTermTestAddMarkFieldsResponseMessage.Group>();
      foreach (var group in groups)
      {
        requestGroups.Add(new FilterTermTestAddMarkFieldsResponseMessage.Group
        {
          GroupId = (int)group,
          GroupName = group.ToString("G")
        });
      }

      return requestGroups.GroupBy(x => x.GroupId).Select(y => y.First());
    }

    private IEnumerable<FilterTermTestAddMarkFieldsResponseMessage.Subject> ToRequestSubjects(List<Subject> subjects)
    {
      var requestSubjects = new Collection<FilterTermTestAddMarkFieldsResponseMessage.Subject>();
      foreach (var subject in subjects)
      {
        requestSubjects.Add(new FilterTermTestAddMarkFieldsResponseMessage.Subject
        {
          SubjectId = subject.Id,
          SubjectName = subject.SubjectName
        });
      }
      return requestSubjects.GroupBy(x => x.SubjectId).Select(y => y.First());
    }

    private IEnumerable<FilterTermTestAddMarkFieldsResponseMessage.Class> ToRequestClasses(List<Class> classes)
    {
      var requestClasses = new Collection<FilterTermTestAddMarkFieldsResponseMessage.Class>();
      foreach (var @class in classes)
      {
        requestClasses.Add(new FilterTermTestAddMarkFieldsResponseMessage.Class
        {
          ClassId = @class.Id,
          ClassName = @class.ClassName
        });
      }
      return requestClasses.GroupBy(x => x.ClassId).Select(y => y.First());
    }
  }
}