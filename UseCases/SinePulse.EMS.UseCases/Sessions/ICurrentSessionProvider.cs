using System.Threading;
using System.Threading.Tasks;
using SinePulse.EMS.Domain.Academic;

namespace SinePulse.EMS.UseCases.Sessions
{
  public interface ICurrentSessionProvider
  {
    Session GetCurrentSession(long branchMediumId);

    Session GetCurrentSessionOrDefault(long branchMediumId);

    Task<Session> GetCurrentSessionAsync(long branchMediumId,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<Session> GetCurrentSessionOrDefaultAsync(long branchMediumId,
      CancellationToken cancellationToken = default(CancellationToken));
  }
}