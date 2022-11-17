using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class DisplayAddExamTypePageUseCase : IDisplayAddExamTypePageUseCase
  {
    private readonly IRepository _repository;

    public DisplayAddExamTypePageUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public DisplayAddExamConfigurationPageResponseMessage DisplayAddExamTypePage(
      DisplayAddExamConfigurationPageRequestMessage requestMessage)
    {
      var includes = new[] { nameof(ClassSubject.Class), nameof(ClassSubject.Subject) };
      var classSubjects = _repository.Table<ClassSubject>(includes).ToList();

      if (requestMessage.ClassId == null && requestMessage.GroupId == null && requestMessage.SubjectId == null)
      {
        List<Class> enumerable = classSubjects.Select(s => s.Class).OrderBy(o => o.ClassCode).Distinct().ToList();
        return new DisplayAddExamConfigurationPageResponseMessage(enumerable.Select(ToRequestClass).ToList(), null, null);
      }

      if (requestMessage.ClassId != null && requestMessage.SubjectId == null && requestMessage.GroupId == null)
      {
        var enumerable = classSubjects.Where(x=>x.Class.Id == requestMessage.ClassId).Select(s => s.Group).Distinct().OrderBy(o=>o.ToString()).ToList();
        return new DisplayAddExamConfigurationPageResponseMessage(null, null, enumerable.Select(ToRequestGroup).ToList());
      }

      if (requestMessage.ClassId != null && requestMessage.GroupId != null && requestMessage.SubjectId == null)
      {
        var enumerable = classSubjects.Where(x => x.Class.Id == requestMessage.ClassId && x.Group == (Domain.Enums.GroupEnum) requestMessage.GroupId.Value).Select(s => s.Subject).Distinct().OrderBy(o => o.SubjectCode).ToList();
        return new DisplayAddExamConfigurationPageResponseMessage(null, enumerable.Select(ToRequestSubject).ToList(),null );
      }

      return new DisplayAddExamConfigurationPageResponseMessage(null, null, null); ;
    }

    private DisplayAddExamConfigurationPageResponseMessage.Subject ToRequestSubject(Subject subject)
    {
      return new DisplayAddExamConfigurationPageResponseMessage.Subject
      {
        SubjectId = subject.Id,
        SubjectName = subject.SubjectName
      };
    }

    private static DisplayAddExamConfigurationPageResponseMessage.Class ToRequestClass(Class @class)
    {
      return new DisplayAddExamConfigurationPageResponseMessage.Class
      {
        ClassId = @class.Id,
        ClassName = @class.ClassName
      };
    }

    private static DisplayAddExamConfigurationPageResponseMessage.Group ToRequestGroup(Domain.Enums.GroupEnum group)
    {
      return new DisplayAddExamConfigurationPageResponseMessage.Group
      {
        GroupId = (long)group,
        GroupName = group.ToString()
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