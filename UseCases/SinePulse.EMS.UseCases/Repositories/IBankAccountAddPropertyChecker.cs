namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IBankAccountAddPropertyChecker
  {
    bool IsCharOfAccountImported(long bankBranchId);
    bool IsCurrentSessionExists(long bankBranchId);
  }
}