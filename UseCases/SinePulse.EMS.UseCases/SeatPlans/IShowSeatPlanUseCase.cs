using SinePulse.EMS.Messages.SeatPlanMessages;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public interface IShowSeatPlanUseCase
  {
    ShowSeatPlanResponseMessage ShowSeatPlan(ShowSeatPlanRequestMessage message);
  }
}