namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueJobTypePropertyChecker
  {
    bool IsUniqueJobTypeTitle(string title);
    bool IsUniqueJobTypeTitle(string title, long jobTypeId);
  }
}