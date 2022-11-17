using FluentValidation;
using SinePulse.EMS.Messages.LogoutMessages;

namespace SinePulse.EMS.UseCases.Logout
{
  public class LogoutRequestMessageValidator : AbstractValidator<LogoutRequestMessage>
  {
  }
}