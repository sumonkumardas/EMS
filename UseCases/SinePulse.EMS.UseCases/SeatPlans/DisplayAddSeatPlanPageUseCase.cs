using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class DisplayAddSeatPlanPageUseCase : IDisplayAddSeatPlanPageUseCase
  {
    private readonly IRepository _repository;
    private readonly ITotalRegisteredStudentProvider _totalRegisteredStudentProvider;
    private readonly ITotalSeatPlanedStudentProvider _totalSeatPlanedStudentProvider;

    public DisplayAddSeatPlanPageUseCase(IRepository repository,
      ITotalRegisteredStudentProvider totalRegisteredStudentProvider,
      ITotalSeatPlanedStudentProvider totalSeatPlanedStudentProvider)
    {
      _repository = repository;
      _totalRegisteredStudentProvider = totalRegisteredStudentProvider;
      _totalSeatPlanedStudentProvider = totalSeatPlanedStudentProvider;
    }

    public DisplayAddSeatPlanPageResponseMessage DisplayAddSeatPlanPage(
      DisplayAddSeatPlanPageRequestMessage requestMessage)
    {
      var test = _repository.GetById<TermTest, ExamConfiguration, ExamTerm, Session, BranchMedium, Branch>(
        requestMessage.TestId,
        x => x.ExamConfiguration,
        x => x.ExamConfiguration.Term,
        x => x.ExamConfiguration.Term.Session,
        x => x.ExamConfiguration.Term.Session.BranchMedium,
        x => x.ExamConfiguration.Term.Session.BranchMedium.Branch);

      var rooms = _repository.Filter<Room>(x => x.Branch.Id == test.ExamConfiguration.Term.Session.BranchMedium.Branch.Id)
        .AsEnumerable().Select(ToRequestObject).ToList();

      var registeredStudent = _totalRegisteredStudentProvider.GetTotalRegisteredStudentOfTest(test.Id).Result;
      var seatPlanedStudent = _totalSeatPlanedStudentProvider.GetTotalSeatPlanedStudentOfTest(test.Id).Result;
      var remainingStudent = registeredStudent - seatPlanedStudent;
      var examTermId = test.ExamConfiguration.Term.Id;
      return new DisplayAddSeatPlanPageResponseMessage(rooms, remainingStudent,examTermId);
    }

    private DisplayAddSeatPlanPageResponseMessage.Room ToRequestObject(Room room)
    {
      var bookedSeat = _totalSeatPlanedStudentProvider.GetTotalSeatPlanedStudentOfRoom(room.Id).Result;
      return new DisplayAddSeatPlanPageResponseMessage.Room
      {
        RoomId = room.Id,
        RoomNo = room.RoomNo,
        TotalSeat = room.ExamTimeCapacity,
        AvailableSeat = room.ExamTimeCapacity - bookedSeat
      };
    }
  }
}