namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IValidTermChecker
  {
    bool IsValidTerm(long termId);
  }
}