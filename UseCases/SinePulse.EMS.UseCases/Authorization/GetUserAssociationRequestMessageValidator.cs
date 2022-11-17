using FluentValidation;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Authorization
{
  public class GetUserAssociationRequestMessageValidator : AbstractValidator<GetUserAssociationRequestMessage>
  {
    private readonly IValidUsernameChecker _validUsernameChecker;

    public GetUserAssociationRequestMessageValidator(IValidUsernameChecker validUsernameChecker)
    {
      _validUsernameChecker = validUsernameChecker;

      RuleFor(_ => _.Username).NotNull();
      RuleFor(_ => _.Username).NotEmpty();
      RuleFor(_ => _.Username).Must(IsValidUsername)
        .WithMessage("Username does not exists.");
    }

    private bool IsValidUsername(string username)
    {
      return _validUsernameChecker.IsValidUsername(username);
    }
  }
}