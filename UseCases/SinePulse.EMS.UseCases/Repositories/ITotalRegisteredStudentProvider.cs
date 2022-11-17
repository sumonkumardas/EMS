using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface ITotalRegisteredStudentProvider
  {
    Task<int> GetTotalRegisteredStudentOfTest(long testId);
  }
}