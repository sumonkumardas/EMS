using SinePulse.EMS.Messages.SeatPlanMessages;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public interface IDisplayEditSeatPlanPageUseCase
  {
    DisplayEditSeatPlanPageResponseMessage DisplayEditSeatPlanPage(
      DisplayEditSeatPlanPageRequestMessage requestMessage);
  }
}