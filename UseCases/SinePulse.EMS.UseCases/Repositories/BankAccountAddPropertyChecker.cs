using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class BankAccountAddPropertyChecker : IBankAccountAddPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public BankAccountAddPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsCharOfAccountImported(long bankBranchId)
    {
      var currentSession = GetCurrentSession(bankBranchId);
      if (currentSession != null)
        return _dbContext.BranchMediumAccountsHeads.Any(b => b.Session.Id == currentSession.Id);
      return false;
    }

    public bool IsCurrentSessionExists(long bankBranchId)
    {
      var currentSession = GetCurrentSession(bankBranchId);
      if (currentSession != null)
        return true;
      return false;
    }

    private Session GetCurrentSession(long bankBranchId)
    {
      return _dbContext.BankBranches
        .Include(nameof(Bank))
        .Include(nameof(Bank) + "." + nameof(BranchMedium))
        .Include(nameof(Bank) + "." + nameof(BranchMedium) + "." + nameof(BranchMedium.Sessions))
        .FirstOrDefault(b => b.Id == bankBranchId)?
        .Bank?
        .BranchMedium?
        .Sessions?
        .FirstOrDefault(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now);
    }
  }
}