using SinePulse.EMS.Messages.SeatPlanMessages;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public interface IAddSeatPlanUseCase
  {
    AddSeatPlanResponseMessage AddSeatPlan(AddSeatPlanRequestMessage requestMessage);
  }
}