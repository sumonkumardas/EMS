using SinePulse.EMS.Messages.SeatPlanMessages;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public interface IDisplayAddSeatPlanPageUseCase
  {
    DisplayAddSeatPlanPageResponseMessage DisplayAddSeatPlanPage(DisplayAddSeatPlanPageRequestMessage requestMessage);
  }
}