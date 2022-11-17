using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class ShowSeatPlanUseCase : IShowSeatPlanUseCase
  {
    private readonly IRepository _repository;

    public ShowSeatPlanUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowSeatPlanResponseMessage ShowSeatPlan(ShowSeatPlanRequestMessage message)
    {
      var includes = new[]
      {
        $"{nameof(SeatPlan.Test)}.{nameof(ExamConfiguration)}",
        nameof(SeatPlan.Room)
      };
      var markEntity = _repository.GetById<SeatPlan>(message.SeatPlanId, includes);
      return new ShowSeatPlanResponseMessage(SeatPlanEntity(markEntity));
    }

    private ShowSeatPlanResponseMessage.SeatPlan SeatPlanEntity(SeatPlan seatPlanEntity)
    {
      return new ShowSeatPlanResponseMessage.SeatPlan
      {
        Id = seatPlanEntity.Id,
        RollRange = seatPlanEntity.RollRange,
        TotalStudent = seatPlanEntity.TotalStudent,
        Status = ConvertToMessageStatus(seatPlanEntity.Status),
        Test = new ShowSeatPlanResponseMessage.Test
        {
          TestId = seatPlanEntity.Test.Id,
          TestName = seatPlanEntity.Test.ExamConfiguration.Title()
        },
        Room = new ShowSeatPlanResponseMessage.Room
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