using System;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class AddClassTestUseCase : IAddClassTestUseCase
  {
    private readonly IRepository _repository;
    private readonly IMessageSender _messageSender;

    public AddClassTestUseCase(IRepository repository, IMessageSender messageSender)
    {
      _repository = repository;
      _messageSender = messageSender;
    }

    public ClassTest AddClassTest(AddClassTestRequestMessage requestMessage)
    {
      var examConfiguration = _repository.GetById<ExamConfiguration>(requestMessage.ExamConfigurationId);
      var section = _repository.GetById<Section>(requestMessage.SectionId);
      var test = new ClassTest
      {
        TestName = requestMessage.TestName,
        FullMarks = requestMessage.FullMarks,
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

        ExamConfiguration = examConfiguration,
        Section = section
      };

      _repository.Insert(test);

      SendConfigureClassTestStartedAlertMessage(test.Id);

      return test;
    }

    private void SendConfigureClassTestStartedAlertMessage(long classTestId)
    {
      var message = new ConfigureClassTestStartedAlertMessage {ClassTestId = classTestId};
      _messageSender.Send(message, MicroServiceAddresses.AlertProcessingService)
        .GetAwaiter().GetResult();
    }
  }
}