using FluentValidation;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Roles
{
  public class AddRoleRequestMessageValidator : AbstractValidator<AddRoleRequestMessage>
  {
    private readonly IUniqueRolePropertyChecker _rolePropertyChecker;

    public AddRoleRequestMessageValidator(IUniqueRolePropertyChecker rolePropertyChecker)
    {
      _rolePropertyChecker = rolePropertyChecker;
      RuleFor(x => x.RoleName).NotEmpty().WithMessage("Please specify Role name");
      RuleFor(x => x.RoleName).NotNull().WithMessage("Please specify Role name");
      RuleFor(x => x).Must(x => IsUniqueRoleName(x.RoleName)).WithMessage("Role Name must be unique");
    }

    private bool IsUniqueRoleName(string roleName)
    {
      return _rolePropertyChecker.IsUniqueRoleName(roleName);
    }
  }
}