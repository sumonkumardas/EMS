using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IEditSeatPlanValidationHelper
  {
    Task<bool> CanEditStudentInExamRoom(long roomId, int totalStudent, long seatPlanId);

    Task<bool> CanEditStudentFromTermTest(long testId, int totalStudent, long seatPlanId);

    Task<bool> CanEditStudentFromSameTermTest(int totalStudent, long seatPlanId);
  }
}