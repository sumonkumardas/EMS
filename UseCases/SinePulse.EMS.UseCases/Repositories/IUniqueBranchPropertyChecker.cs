namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueBranchPropertyChecker
  {
    bool IsUniqueBranchName(string branchName);
    bool IsUniqueBranchCode(string branchCode);
    bool IsUniqueBranchName(string branchName, long branchId);
    bool IsUniqueBranchCode(string branchCode, long branchId);
  }
}