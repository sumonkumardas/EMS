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
  public class AddTermTestUseCase : IAddTermTestUseCase
  {
    private readonly IRepository _repository;
    private readonly IMessageSender _messageSender;

    public AddTermTestUseCase(IRepository repository, IMessageSender messageSender)
    {
      _repository = repository;
      _messageSender = messageSender;
    }

    public void AddTermTest(AddTermTestRequestMessage requestMessage)
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

      var test = new TermTest
      {
        StartTimestamp = requestMessage.Date + requestMessage.StartTime,
        EndTimestamp = requestMessage.Date + requestMessage.EndTime,
        ExamType = requestMessage.ExamType,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },
        ExamConfiguration = examConfiguration
      };

      _repository.Insert(test);

      SendConfigureTermTestStartedAlertMessage(test.Id);
    }

    private void SendConfigureTermTestStartedAlertMessage(long termTestId)
    {
      var message = new ConfigureTermTestStartedAlertMessage {TermTestId = termTestId};
      _messageSender.Send(message, MicroServiceAddresses.AlertProcessingService)
        .GetAwaiter().GetResult();
    }
  }
}