using System;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.Terms
{
  public class DisplayAddTermPageUseCase : IDisplayAddTermPageUseCase
  {
    private readonly IShowCurrentSessionListUseCase _sessionProvider;
    private readonly IRepository _repository;

    public DisplayAddTermPageUseCase(IShowCurrentSessionListUseCase currentSessionProvider, IRepository repository)
    {
      _sessionProvider = currentSessionProvider;
      _repository = repository;
    }

    public DisplayAddTermPageResponseMessage DisplayAddTermPage(DisplayAddTermPageRequestMessage requestMessage)
    {
      var sessions = _sessionProvider.ShowSessionList(requestMessage.BranchMediumId);
      return new DisplayAddTermPageResponseMessage(sessions.ToList());
    }
  }
}