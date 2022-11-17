using FluentValidation;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Roles
{
  public class ShowRoleRequestMessageValidator : AbstractValidator<ShowRoleRequestMessage>
  {
    private readonly IValidRoleChecker _validRoleChecker;

    public ShowRoleRequestMessageValidator(IValidRoleChecker validRoleChecker)
    {
      _validRoleChecker = validRoleChecker;

      RuleFor(x => x.RoleId).Must(IsValidRole).WithMessage("This mark doesn't exists");
    }

    private bool IsValidRole(string termId)
    {
      return _validRoleChecker.IsValidRole(termId);
    }
  }
}