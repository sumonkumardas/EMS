using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class ShowCurrentSessionListUseCase : IShowCurrentSessionListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public ShowCurrentSessionListUseCase(IRepository repository, ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Session, SessionMessageModel>());
    }

    public IEnumerable<SessionMessageModel> ShowSessionList(long branchMediumId)
    {
      var mapper = _autoMapperConfig.CreateMapper();
      var currentSession = _currentSessionProvider.GetCurrentSession(branchMediumId);
      var sessions = _repository.Table<Session>().Where(w => currentSession != null && w.BranchMedium.Id == branchMediumId & w.StartDate >= currentSession.StartDate).OrderBy(o => o.StartDate).ToList();
      return mapper.Map(sessions, new List<SessionMessageModel>());
    }
  }
}
