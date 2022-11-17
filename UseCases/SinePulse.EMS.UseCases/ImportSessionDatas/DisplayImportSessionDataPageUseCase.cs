using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.ImportSessionDataMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.ImportSessionDatas
{
  public class DisplayImportSessionDataPageUseCase : IDisplayImportSessionDataPageUseCase
  {
    private readonly IRepository _repository;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public DisplayImportSessionDataPageUseCase(IRepository repository,
      ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
    }

    public DisplayImportSessionDataPageResponseMessage DisplayImportSessionDataPage(
      DisplayImportSessionDataPageRequestMessage requestMessage)
    {
      var currentSession = ToRequestObject(_currentSessionProvider.GetCurrentSession(requestMessage.BranchMediumId));
      var previousSessions = _repository.Filter<Session>(
          x => x.BranchMedium.Id == requestMessage.BranchMediumId && x.EndDate <= currentSession.StartDate)
        .AsEnumerable().Select(ToRequestObject).ToList();

      return new DisplayImportSessionDataPageResponseMessage(previousSessions, currentSession);
    }

    private DisplayImportSessionDataPageResponseMessage.Session ToRequestObject(Session session)
    {
      return new DisplayImportSessionDataPageResponseMessage.Session
      {
        SessionId = session.Id,
        SessionName = session.SessionName,
        StartDate = session.StartDate,
        EndDate = session.EndDate
      };
    }
  }
}