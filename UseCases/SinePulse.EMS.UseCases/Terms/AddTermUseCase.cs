using System;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.UseCases.Terms
{
  public class AddTermUseCase : IAddTermUseCase
  {
    private readonly IRepository _repository;
    private readonly IMessageSender _messageSender;

    public AddTermUseCase(IRepository repository, IMessageSender messageSender)
    {
      _repository = repository;
      _messageSender = messageSender;
    }

    public ExamTerm AddTerm(AddTermRequestMessage requestMessage)
    {
      var session = _repository.GetById<Session>(requestMessage.SessionId);
      var branchMedium = _repository.GetById<BranchMedium>(requestMessage.BranchMediumId);
      var term = new ExamTerm
      {
        TermName = requestMessage.TermName,
        StartDate = requestMessage.StartDate,
        EndDate = requestMessage.EndDate,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },

        Session = session,
      };

      _repository.Insert(term);
      
      SendConfigureTermStartedAlertMessage(term.Id);

      return term;
    }

    private void SendConfigureTermStartedAlertMessage(long termId)
    {
      var message = new ConfigureTermStartedAlertMessage {TermId = termId};
      _messageSender.Send(message, MicroServiceAddresses.AlertProcessingService)
        .GetAwaiter().GetResult();
    }
  }
}