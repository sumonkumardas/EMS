using SinePulse.EMS.Messages.SeatPlanMessages;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public interface IShowSeatPlanListUseCase
  {
    ShowSeatPlanListResponseMessage ShowSeatPlanList(ShowSeatPlanListRequestMessage message);
  }
}