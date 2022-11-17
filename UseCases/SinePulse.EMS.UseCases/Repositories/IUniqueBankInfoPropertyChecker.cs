namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueBankInfoPropertyChecker
  {
    bool IsUniqueBankAccountNumber(string accountNumber);
    bool IsUniqueBankName(string bankName);
    bool IsUniqueBankBranchName(string branchName);
    bool IsUniqueBankName(string bankName, long bankId);
    bool IsUniqueBankBranchName(string branchName, long branchId);
    bool IsUniqueBankAccountNumber(string accountNumber, long bankAccountId);
  }
}