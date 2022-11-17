using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class CurrentSessionProvider : ICurrentSessionProvider
  {
    private readonly IRepository _repository;

    public CurrentSessionProvider(IRepository repository)
    {
      _repository = repository;
    }

    public Session GetCurrentSession(long branchMediumId)
    {
      var branchMedium = _repository.GetById<BranchMedium>(branchMediumId);
      if (branchMedium == default(BranchMedium))
        throw new Exception($"No Branch Medium Found with Id: {branchMediumId}");

      return _repository.Filter(CurrentSessionCondition(branchMediumId, branchMedium.SessionBufferPeriods))
        .FirstOrDefault();
    }

    public Session GetCurrentSessionOrDefault(long branchMediumId)
    {
      var branchMedium = _repository.GetById<BranchMedium>(branchMediumId);
      if (branchMedium == default(BranchMedium))
        return default(Session);

      return _repository.Filter(CurrentSessionCondition(branchMediumId, branchMedium.SessionBufferPeriods))
        .FirstOrDefault();
    }

    public async Task<Session> GetCurrentSessionAsync(long branchMediumId,
      CancellationToken cancellationToken = default(CancellationToken))
    {
      var branchMedium = await _repository.GetByIdAsync<BranchMedium>(branchMediumId, cancellationToken);
      if (branchMedium == default(BranchMedium))
        throw new Exception($"No Branch Medium Found with Id: {branchMediumId}");

      return await _repository.Filter(CurrentSessionCondition(branchMediumId, branchMedium.SessionBufferPeriods))
        .FirstAsync(cancellationToken);
    }

    public async Task<Session> GetCurrentSessionOrDefaultAsync(long branchMediumId,
      CancellationToken cancellationToken = default(CancellationToken))
    {
      var branchMedium = await _repository.GetByIdAsync<BranchMedium>(branchMediumId, cancellationToken);
      if (branchMedium == default(BranchMedium))
        return default(Session);

      return await _repository.Filter(CurrentSessionCondition(branchMediumId, branchMedium.SessionBufferPeriods))
        .FirstOrDefaultAsync(cancellationToken);
    }

    private static Expression<Func<Session, bool>> CurrentSessionCondition(long branchMediumId,
      int sessionBufferDays)
    {
      var bufferedToday = DateTime.Today - TimeSpan.FromDays(sessionBufferDays);

      return x => x.BranchMedium.Id == branchMediumId
                  && x.Status == StatusEnum.Active
                  && !x.IsSessionClosed
                  && x.StartDate <= DateTime.Today && bufferedToday <= x.EndDate;
    }
  }
}