using System;
using System.Linq;
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

    public DisplayAddExamTypePageResponseMessage DisplayAddExamTypePage(
      DisplayAddExamTypePageRequestMessage requestMessage)
    {
      var includes = new[] {nameof(ClassSubject.Class), nameof(ClassSubject.Subject)};
      var groupSubjects = _repository.Table<ClassSubject>(includes).ToList();
      return new DisplayAddExamTypePageResponseMessage(groupSubjects.Select(ToRequestObject).ToList());
    }

    private static DisplayAddExamTypePageResponseMessage.GroupSubject ToRequestObject(ClassSubject groupSubjects)
    {
      return new DisplayAddExamTypePageResponseMessage.GroupSubject
      {
        GroupSubjectId = groupSubjects.Id,
        Group = ToMessageEnum(groupSubjects.Group),
        ClassName = groupSubjects.Class.ClassName,
        SubjectName = groupSubjects.Subject.SubjectName
      };
    }

    private static GroupEnum ToMessageEnum(Domain.Enums.GroupEnum group)
    {
      switch (group)
      {
        case Domain.Enums.GroupEnum.AllGroup:
          return GroupEnum.AllGroup;
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