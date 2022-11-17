using FluentValidation;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.SeatPlans
{
  public class ShowSeatPlanListRequestMessageValidator : AbstractValidator<ShowSeatPlanListRequestMessage>
  {

    public ShowSeatPlanListRequestMessageValidator()
    {
    }
  }
}