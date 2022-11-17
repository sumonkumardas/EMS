using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IAddSeatPlanValidationHelper
  {
    Task<bool> CanAddStudentInExamRoom(long roomId, int totalStudent);

    Task<bool> CanAddStudentFromTermTest(long testId, int totalStudent);
  }
}