using System;
using System.Linq;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class EditTermTestUseCase : IEditTermTestUseCase
  {
    private readonly IRepository _repository;
    private readonly IMessageSender _messageSender;

    public EditTermTestUseCase(IRepository repository, IMessageSender messageSender)
    {
      _repository = repository;
      _messageSender = messageSender;
    }

    public void EditTermTest(EditTermTestRequestMessage requestMessage)
    {
      var includes = new string[]
      {
        nameof(ExamConfiguration.Class),
        nameof(ExamConfiguration.Subject),
        nameof(ExamConfiguration.Term)
      };
      var examConfiguration = _repository.Table<ExamConfiguration>(includes)
        .FirstOrDefault(x => x.Subject.Id == requestMessage.SubjectId &&
                             x.Class.Id == requestMessage.ClassId &&
                             x.Term.Id == requestMessage.TermId);

      var termTest = _repository.GetById<TermTest>(requestMessage.Id);

      termTest.StartTimestamp = requestMessage.Date + requestMessage.StartTime;
      termTest.EndTimestamp = requestMessage.Date + requestMessage.EndTime;
      termTest.ExamType = requestMessage.ExamType;
      termTest.Status = StatusEnum.Active;

      termTest.AuditFields.LastUpdatedBy = requestMessage.CurrentUserName;
      termTest.AuditFields.LastUpdatedDateTime = DateTime.Now;
      termTest.ExamConfiguration = examConfiguration;
    }
  }
}