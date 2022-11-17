using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class ShowSeatPlanListUseCase : IShowSeatPlanListUseCase
  {
    private readonly IRepository _repository;

    public ShowSeatPlanListUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowSeatPlanListResponseMessage ShowSeatPlanList(ShowSeatPlanListRequestMessage message)
    {
      var includes = new[]
      {
        $"{nameof(SeatPlan.Test)}.{nameof(ExamConfiguration)}",
        $"{nameof(SeatPlan.Test)}.{nameof(ExamConfiguration)}.{nameof(ExamConfiguration.Term)}",
        $"{nameof(SeatPlan.Test)}.{nameof(ExamConfiguration)}.{nameof(ExamConfiguration.Class)}",
        $"{nameof(SeatPlan.Test)}.{nameof(ExamConfiguration)}.{nameof(ExamConfiguration.Subject)}",
        nameof(SeatPlan.Room)
      };
      var seats = _repository.Table<SeatPlan>(includes).Where(x=>x.Test.ExamConfiguration.Term.Id == message.TermId);
      var seatCollection = new List<ShowSeatPlanListResponseMessage.SeatPlan>();
      foreach (var seat in seats)
      {
        seatCollection.Add(SeatPlanEntity(seat));
      }
      return new ShowSeatPlanListResponseMessage(seatCollection);
    }

    private ShowSeatPlanListResponseMessage.SeatPlan SeatPlanEntity(SeatPlan seatPlanEntity)
    {
      return new ShowSeatPlanListResponseMessage.SeatPlan
      {
        Id = seatPlanEntity.Id,
        RollRange = seatPlanEntity.RollRange,
        TotalStudent = seatPlanEntity.TotalStudent,
        Status = ConvertToMessageStatus(seatPlanEntity.Status),
        Test = new ShowSeatPlanListResponseMessage.Test
        {
          TestId = seatPlanEntity.Test.Id,
          ClassName = seatPlanEntity.Test.ExamConfiguration.Class.ClassName,
          SubjectName = seatPlanEntity.Test.ExamConfiguration.Subject.SubjectName,
          ExamType = seatPlanEntity.Test.ExamType.ToString(),
          TestStartDate = seatPlanEntity.Test.StartTimestamp,
          TestEndDate = seatPlanEntity.Test.EndTimestamp
        },
        Room = new ShowSeatPlanListResponseMessage.Room
        {
          RoomId = seatPlanEntity.Room.Id,
          RoomNo = seatPlanEntity.Room.RoomNo
        }
      };
    }

    private static Messages.Model.Enums.StatusEnum ConvertToMessageStatus(StatusEnum entityStatus)
    {
      switch (entityStatus)
      {
        case StatusEnum.Active:
          return Messages.Model.Enums.StatusEnum.Active;
        case StatusEnum.Inactive:
          return Messages.Model.Enums.StatusEnum.Inactive;
        case StatusEnum.Pending:
          return Messages.Model.Enums.StatusEnum.Pending;
        case StatusEnum.Transferred:
          return Messages.Model.Enums.StatusEnum.Transferred;
        case StatusEnum.Passed:
          return Messages.Model.Enums.StatusEnum.Passed;
        default:
          throw new ArgumentOutOfRangeException(nameof(entityStatus), entityStatus, null);
      }
    }
  }
}