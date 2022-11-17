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
  public class EditExamConfigurationUseCase : IEditExamConfigurationUseCase
  {
    private readonly IRepository _repository;

    public EditExamConfigurationUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditExamConfiguration(EditExamConfigurationRequestMessage requestMessage)
    {
      var includes = new string[]
      {
        nameof(ClassSubject.Class),
        nameof(ClassSubject.Subject)
      };
      var term = _repository.GetById<ExamTerm>(requestMessage.TermId);
      var subject = _repository.Table<ClassSubject>(includes).FirstOrDefault(x=>x.Subject.Id == requestMessage.SubjectId && x.Class.Id == requestMessage.ClassId && x.Group == (GroupEnum) requestMessage.GroupId);
      var examConfiguration = _repository.GetById<ExamConfiguration>(requestMessage.Id, includes);

      examConfiguration.SubjectiveFullMark = requestMessage.SubjectiveFullMark;
      examConfiguration.ObjectiveFullMark = requestMessage.ObjectiveFullMark;
      examConfiguration.PracticalFullMark = requestMessage.PracticalFullMark;
      examConfiguration.ClassTestPercentage = requestMessage.ClassTestPercentage;
      examConfiguration.ObjectivePassMark = requestMessage.ObjectivePassMark;
      examConfiguration.PracticalPassMark = requestMessage.PracticalPassMark;
      examConfiguration.SubjectivePassMark = requestMessage.SubjectivePassMark;
      examConfiguration.Status = StatusEnum.Active;

      examConfiguration.AuditFields.LastUpdatedBy = requestMessage.CurrentUserName;
      examConfiguration.AuditFields.LastUpdatedDateTime = DateTime.Now;

      examConfiguration.Term = term;
      examConfiguration.Subject = subject.Subject;
      examConfiguration.Class = subject.Class;
    }
  }
}