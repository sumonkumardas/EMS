using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.ScheduleJobService
{
  public class ClosingCandidateSessionProvider : IClosingCandidateSessionProvider
  {
    private readonly IRepository _repository;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public ClosingCandidateSessionProvider(IRepository repository, ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
    }

    public IEnumerable<Session> GetClosingCandidateSessions()
    {
      var branchMediums = _repository.Filter<BranchMedium>(x => true).AsEnumerable();
      return branchMediums.Select(x => _currentSessionProvider.GetCurrentSessionOrDefault(x.Id))
        .Where(x => x != null && x.IsSessionClosing && !x.IsSessionClosed);
    }
  }
}