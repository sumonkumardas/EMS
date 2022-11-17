using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Persistence.Repositories;
using System.Linq;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class EditSessionUseCase : IEditSessionUseCase
  {
    private readonly IRepository _repository;

    public EditSessionUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void UpdateSession(EditSessionRequestMessage requestMessage)
    {
      if (requestMessage.IsSessionClosed)
      {
        UpdateExistingCurrentSession();
      }
      var session = _repository.GetById<Session>(requestMessage.SessionId);
      
      session.SessionName = requestMessage.SessionName;
      session.StartDate = requestMessage.StartDate;
      session.EndDate = requestMessage.EndDate;
      session.Status = requestMessage.Status;
      session.IsSessionClosed = requestMessage.IsSessionClosed;
      session.AuditFields.LastUpdatedBy = requestMessage.CurrentUserName;
      session.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
    
    private void UpdateExistingCurrentSession()
    {
      var currentSession = _repository.Table<Session>().FirstOrDefault(s => s.IsSessionClosed);
      if (currentSession != null)
      {
        currentSession.IsSessionClosed = false;
      }
    }
  }
}