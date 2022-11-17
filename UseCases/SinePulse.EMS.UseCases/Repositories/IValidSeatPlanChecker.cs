namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IValidSeatPlanChecker
  {
    bool IsValidSeatPlan(long seatPlanId);
  }
}