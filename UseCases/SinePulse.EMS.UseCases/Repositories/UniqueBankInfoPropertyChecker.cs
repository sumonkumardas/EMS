using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class UniqueBankInfoPropertyChecker : IUniqueBankInfoPropertyChecker
  {
    private readonly EmsDbContext _dbContext;

    public UniqueBankInfoPropertyChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsUniqueBankAccountNumber(string accountNumber)
    {
      return !_dbContext.BankAccounts.Any(b => b.AccountNumber == accountNumber);
    }

    public bool IsUniqueBankName(string bankName)
    {
      return !_dbContext.Banks.Any(b => b.BankName == bankName);
    }

    public bool IsUniqueBankBranchName(string branchName)
    {
      return !_dbContext.BankBranches.Any(b => b.BranchName == branchName);
    }

    public bool IsUniqueBankName(string bankName, long bankId)
    {
      return !_dbContext.Banks.Any(b => b.BankName == bankName && b.Id != bankId);
    }

    public bool IsUniqueBankBranchName(string branchName, long branchId)
    {
      return !_dbContext.BankBranches.Any(b => b.BranchName == branchName && b.Id != branchId);
    }

    public bool IsUniqueBankAccountNumber(string accountNumber, long bankAccountId)
    {
      return !_dbContext.BankAccounts.Any(b => b.AccountNumber == accountNumber && b.Id != bankAccountId);
    }
  }
}