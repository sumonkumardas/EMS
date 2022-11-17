namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IMediumNotPreviouslyAddedChecker
  {
    bool IsMediumPreviouslyAdded(long mediumId, long branchId);
    bool IsMediumPreviouslyAdded(long mediumId, long branchId, long branchMediumId);
  }
}