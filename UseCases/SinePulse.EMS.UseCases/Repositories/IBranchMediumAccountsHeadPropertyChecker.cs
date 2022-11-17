namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IBranchMediumAccountsHeadPropertyChecker
  {
    bool IsAlreadyCOAImportedInSession(long sessionId);
    bool IsAlreadyCOAImportedInSession(long sessionId, long branchMediumId);
  }
}