using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.Repositories
{
  public interface ITotalSeatPlanedStudentProvider
  {
    Task<int> GetTotalSeatPlanedStudentOfTest(long testId);
    Task<int> GetTotalSeatPlanedStudentOfRoom(long roomId);
    Task<int> GetTotalSeatPlanedStudentOfTestExcept(long testId, long seatPlanId);
    Task<int> GetTotalSeatPlanedStudentOfRoomExcept(long roomId, long seatPlanId);
  }
}