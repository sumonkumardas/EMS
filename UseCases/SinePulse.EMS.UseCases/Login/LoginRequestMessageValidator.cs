using FluentValidation;
using SinePulse.EMS.Messages.LoginMessages;

namespace SinePulse.EMS.UseCases.Login
{
  public class LoginRequestMessageValidator : AbstractValidator<LoginRequestMessage>
  {
    public LoginRequestMessageValidator()
    {
      RuleFor(x => x.UserName).NotEmpty().WithMessage("Please specify User Name.");
      RuleFor(x => x.UserName).NotNull().WithMessage("Please specify User Name.");
      
      RuleFor(x => x.Password).NotEmpty().WithMessage("Please specify Password.");
      RuleFor(x => x.Password).NotNull().WithMessage("Please specify Password.");
    }
  }
}