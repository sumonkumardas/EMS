using FluentValidation;
using SinePulse.EMS.Messages.BreakTimeMessages;

namespace SinePulse.EMS.UseCases.BreakTimes
{
  public class GetClassBreakTimeRequestMessageValidator : AbstractValidator<GetClassBreakTimeRequestMessage>
  {
    public GetClassBreakTimeRequestMessageValidator()
    {
    }
  }
}