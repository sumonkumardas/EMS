namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IValidBranchMediumChecker
  {
    bool IsValidBranchMedium(long branchMediumId);
  }
}