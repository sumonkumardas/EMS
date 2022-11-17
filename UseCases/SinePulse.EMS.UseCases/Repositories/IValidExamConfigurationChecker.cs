namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IValidExamConfigurationChecker
  {
    bool IsValidExamConfiguration(long examConfigurationId);
  }
}