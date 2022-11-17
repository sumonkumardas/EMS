namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueClassTestPropertyChecker
  {
    bool IsUniqueClassTestName(string testName, long sectionId, long examConfigurationId);
    bool IsUniqueClassTestName(string testName, long sectionId, long examConfigurationId,long classTestId);
  }
}