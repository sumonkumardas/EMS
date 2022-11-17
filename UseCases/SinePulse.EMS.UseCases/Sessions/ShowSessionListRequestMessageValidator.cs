using FluentValidation;
using SinePulse.EMS.Messages.SessionMessages;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class ShowSessionListRequestMessageValidator : AbstractValidator<ShowSessionListRequestMessage>
  {
    public ShowSessionListRequestMessageValidator()
    {
    }
  }
}