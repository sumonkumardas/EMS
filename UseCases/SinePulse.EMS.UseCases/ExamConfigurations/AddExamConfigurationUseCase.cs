using System;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class AddExamConfigurationUseCase : IAddExamConfigurationUseCase
  {
    private readonly IRepository _repository;

    public AddExamConfigurationUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ExamConfiguration AddExamConfiguration(AddExamConfigurationRequestMessage requestMessage)
    {
      var inclues = new string[]
      {
        nameof(ClassSubject.Class),
        nameof(ClassSubject.Subject)
      };
      var term = _repository.GetById<ExamTerm>(requestMessage.TermId);
      var subject = _repository.Table<ClassSubject>(inclues).FirstOrDefault(x=>x.Subject.Id == requestMessage.SubjectId && x.Class.Id == requestMessage.ClassId && x.Group == (GroupEnum) requestMessage.GroupId);
      var examConfiguration = new ExamConfiguration
      {
        SubjectiveFullMark = requestMessage.SubjectiveFullMark,
        ObjectiveFullMark = requestMessage.ObjectiveFullMark,
        PracticalFullMark = requestMessage.PracticalFullMark,
        ClassTestPercentage = requestMessage.ClassTestPercentage,
        ObjectivePassMark = requestMessage.ObjectivePassMark,
        PracticalPassMark = requestMessage.PracticalPassMark,
        SubjectivePassMark = requestMessage.SubjectivePassMark,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },

        Term = term,
        Subject = subject.Subject,
        Class = subject.Class,
      };

      _repository.Insert(examConfiguration);

      return examConfiguration;
    }
  }
}