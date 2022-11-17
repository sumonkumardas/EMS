using System.Threading.Tasks;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class SeatPlanValidationHelper : IAddSeatPlanValidationHelper, IEditSeatPlanValidationHelper
  {
    private readonly IRepository _repository;
    private readonly ITotalSeatPlanedStudentProvider _totalSeatPlanedStudentProvider;
    private readonly ITotalRegisteredStudentProvider _totalRegisteredStudentProvider;

    public SeatPlanValidationHelper(IRepository repository,
      ITotalSeatPlanedStudentProvider totalSeatPlanedStudentProvider,
      ITotalRegisteredStudentProvider totalRegisteredStudentProvider)
    {
      _repository = repository;
      _totalSeatPlanedStudentProvider = totalSeatPlanedStudentProvider;
      _totalRegisteredStudentProvider = totalRegisteredStudentProvider;
    }

    public async Task<bool> CanAddStudentInExamRoom(long roomId, int totalStudent)
    {
      var room = await _repository.GetByIdAsync<Room>(roomId);
      var roomCapacity = room.ExamTimeCapacity;
      var alreadySeatPlanedStudent =
        await _totalSeatPlanedStudentProvider.GetTotalSeatPlanedStudentOfRoom(roomId);
      return alreadySeatPlanedStudent + totalStudent <= roomCapacity;
    }

    public async Task<bool> CanAddStudentFromTermTest(long testId, int totalStudent)
    {
      var registeredStudent = await _totalRegisteredStudentProvider.GetTotalRegisteredStudentOfTest(testId);
      var alreadySeatPlanedStudent =
        await _totalSeatPlanedStudentProvider.GetTotalSeatPlanedStudentOfTest(testId);
      return alreadySeatPlanedStudent + totalStudent <= registeredStudent;
    }

    public async Task<bool> CanEditStudentInExamRoom(long roomId, int totalStudent, long seatPlanId)
    {
      var room = await _repository.GetByIdAsync<Room>(roomId);
      var roomCapacity = room.ExamTimeCapacity;
      var alreadySeatPlanedStudent =
        await _totalSeatPlanedStudentProvider.GetTotalSeatPlanedStudentOfRoomExcept(roomId, seatPlanId);
      return alreadySeatPlanedStudent + totalStudent <= roomCapacity;
    }

    public async Task<bool> CanEditStudentFromTermTest(long testId, int totalStudent, long seatPlanId)
    {
      var registeredStudent = await _totalRegisteredStudentProvider.GetTotalRegisteredStudentOfTest(testId);
      var alreadySeatPlanedStudent =
        await _totalSeatPlanedStudentProvider.GetTotalSeatPlanedStudentOfTestExcept(testId, seatPlanId);
      return alreadySeatPlanedStudent + totalStudent <= registeredStudent;
    }

    public async Task<bool> CanEditStudentFromSameTermTest(int totalStudent, long seatPlanId)
    {
      var seatPlan = await _repository.GetByIdAsync<SeatPlan, TermTest>(seatPlanId, x => x.Test);
      return await CanEditStudentFromTermTest(seatPlan.Test.Id, totalStudent, seatPlanId);
    }
  }
}