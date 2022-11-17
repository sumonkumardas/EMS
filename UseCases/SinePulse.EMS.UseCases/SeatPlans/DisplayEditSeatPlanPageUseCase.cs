using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class DisplayEditSeatPlanPageUseCase : IDisplayEditSeatPlanPageUseCase
  {
    private readonly IRepository _repository;
    private readonly ITotalRegisteredStudentProvider _totalRegisteredStudentProvider;
    private readonly ITotalSeatPlanedStudentProvider _totalSeatPlanedStudentProvider;

    public DisplayEditSeatPlanPageUseCase(IRepository repository,
      ITotalRegisteredStudentProvider totalRegisteredStudentProvider,
      ITotalSeatPlanedStudentProvider totalSeatPlanedStudentProvider)
    {
      _repository = repository;
      _totalRegisteredStudentProvider = totalRegisteredStudentProvider;
      _totalSeatPlanedStudentProvider = totalSeatPlanedStudentProvider;
    }

    public DisplayEditSeatPlanPageResponseMessage DisplayEditSeatPlanPage(
      DisplayEditSeatPlanPageRequestMessage requestMessage)
    {
      var seatPlan =
        _repository.GetById<SeatPlan, TermTest, ExamConfiguration, ExamTerm, Session, BranchMedium, Branch>(
          requestMessage.SeatPlanId,
          x => x.Test,
          x => x.Test.ExamConfiguration,
          x => x.Test.ExamConfiguration.Term,
          x => x.Test.ExamConfiguration.Term.Session,
          x => x.Test.ExamConfiguration.Term.Session.BranchMedium,
          x => x.Test.ExamConfiguration.Term.Session.BranchMedium.Branch);
      
      var rooms = _repository.Filter<Room>(
          x => x.Branch.Id == seatPlan.Test.ExamConfiguration.Term.Session.BranchMedium.Branch.Id)
        .AsEnumerable().Select(room => ToRequestObject(room, seatPlan.TotalStudent)).ToList();

      var registeredStudent = _totalRegisteredStudentProvider.GetTotalRegisteredStudentOfTest(seatPlan.Test.Id).Result;
      var seatPlanedStudent = _totalSeatPlanedStudentProvider.GetTotalSeatPlanedStudentOfTest(seatPlan.Test.Id).Result;
      var remainingStudent = registeredStudent - seatPlanedStudent + seatPlan.TotalStudent;
      var termId = seatPlan.Test.ExamConfiguration.Term.Id;
      return new DisplayEditSeatPlanPageResponseMessage(ToRequestObject(seatPlan), rooms, remainingStudent, termId);
    }

    private static DisplayEditSeatPlanPageResponseMessage.SeatPlan ToRequestObject(SeatPlan seatPlan)
    {
      return new DisplayEditSeatPlanPageResponseMessage.SeatPlan
      {
        Id = seatPlan.Id,
        RollRange = seatPlan.RollRange,
        TotalStudent = seatPlan.TotalStudent,
        RoomId = seatPlan.RoomId,
        TestId = seatPlan.TestId
      };
    }

    private DisplayEditSeatPlanPageResponseMessage.Room ToRequestObject(Room room, int allocatedSeat)
    {
      var bookedSeat = _totalSeatPlanedStudentProvider.GetTotalSeatPlanedStudentOfRoom(room.Id).Result;
      return new DisplayEditSeatPlanPageResponseMessage.Room
      {
        RoomId = room.Id,
        RoomNo = room.RoomNo,
        TotalSeat = room.ExamTimeCapacity,
        AvailableSeat = room.ExamTimeCapacity - bookedSeat + allocatedSeat
      };
    }
  }
}