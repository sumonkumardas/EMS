using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class ShowExamConfigurationUseCase : IShowExamConfigurationUseCase
  {
    private readonly IRepository _repository;

    public ShowExamConfigurationUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowExamConfigurationResponseMessage ShowExamConfiguration(ShowExamConfigurationRequestMessage message)
    {
      var includes = new[]
      {
        nameof(ExamConfiguration.Term),
        //nameof(ExamConfiguration.ClassSubject),
        nameof(ExamConfiguration.Class),
        nameof(ExamConfiguration.Subject),
      };
      var examConfigurationEntity = _repository.GetByIdWithInclude<ExamConfiguration>(message.ExamConfigurationId, includes);
      return new ShowExamConfigurationResponseMessage(ExamConfigurationEntity(examConfigurationEntity));
    }

    private ShowExamConfigurationResponseMessage.ExamConfiguration ExamConfigurationEntity(ExamConfiguration examConfigurationEntity)
    {
      return new ShowExamConfigurationResponseMessage.ExamConfiguration
      {
        ExamConfigurationId = examConfigurationEntity.Id,
        ClassTestPercentage = examConfigurationEntity.ClassTestPercentage,
        ObjectiveFullMark = examConfigurationEntity.ObjectiveFullMark,
        ObjectivePassMark = examConfigurationEntity.ObjectivePassMark,
        PracticalFullMark = examConfigurationEntity.PracticalFullMark,
        PracticalPassMark = examConfigurationEntity.PracticalPassMark,
        SubjectiveFullMark = examConfigurationEntity.SubjectiveFullMark,
        SubjectivePassMark = examConfigurationEntity.SubjectivePassMark,
        Status = examConfigurationEntity.Status,
        Term = new ShowExamConfigurationResponseMessage.Term
        {
          TermId = examConfigurationEntity.Term?.Id ?? 0,
          TermName = examConfigurationEntity.Term != null ? examConfigurationEntity.Term.TermName:string.Empty
        },
        Subject = new ShowExamConfigurationResponseMessage.Subject
        {
          SubjectId = examConfigurationEntity.Subject.Id,
          SubjectName = examConfigurationEntity.Subject.SubjectName
        },
        Class = new ShowExamConfigurationResponseMessage.Class
        {
          ClassId = examConfigurationEntity.Class.Id,
          ClassName = examConfigurationEntity.Class.ClassName
        }
      };
    }

    private GroupEnum ToMessageEnum(Domain.Enums.GroupEnum group)
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

    private static StatusEnum ConvertToMessageStatus(Domain.Enums.StatusEnum entityStatus)
    {
      switch (entityStatus)
      {
        case Domain.Enums.StatusEnum.Active:
          return StatusEnum.Active;
        case Domain.Enums.StatusEnum.Inactive:
          return StatusEnum.Inactive;
        case Domain.Enums.StatusEnum.Pending:
          return StatusEnum.Pending;
        case Domain.Enums.StatusEnum.Transferred:
          return StatusEnum.Transferred;
        case Domain.Enums.StatusEnum.Passed:
          return StatusEnum.Passed;
        default:
          throw new ArgumentOutOfRangeException(nameof(entityStatus), entityStatus, null);
      }
    }
  }
}