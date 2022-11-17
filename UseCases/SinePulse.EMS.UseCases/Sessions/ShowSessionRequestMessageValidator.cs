using FluentValidation;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class ShowSessionRequestMessageValidator : AbstractValidator<ViewSessionRequestMessage>
  {
    public ShowSessionRequestMessageValidator(IUniqueSessionPropertyChecker uniqueSessionPropertyChecker)
    {
    }
  }
}