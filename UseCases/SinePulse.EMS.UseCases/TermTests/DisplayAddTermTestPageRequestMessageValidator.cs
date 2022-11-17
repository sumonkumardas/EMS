using FluentValidation;
using SinePulse.EMS.Messages.TermTestMessages;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class DisplayAddTermTestPageRequestMessageValidator : AbstractValidator<DisplayAddTermTestPageRequestMessage>
  {
    public DisplayAddTermTestPageRequestMessageValidator()
    {
    }
    
  }
}