namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueTermPropertyChecker
  {
    bool IsUniqueTermName(string termName, long sessionId,long branchMediumid);
  }
}