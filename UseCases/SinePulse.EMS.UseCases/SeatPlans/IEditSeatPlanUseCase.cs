using SinePulse.EMS.Messages.SeatPlanMessages;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public interface IEditSeatPlanUseCase
  {
    EditSeatPlanResponseMessage EditSeatPlan(EditSeatPlanRequestMessage requestMessage);
  }
}