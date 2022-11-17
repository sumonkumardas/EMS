using FluentValidation;
using SinePulse.EMS.Messages.RoleMessages;

namespace SinePulse.EMS.UseCases.Roles
{
  public class ShowRoleListRequestMessageValidator : AbstractValidator<ShowRoleListRequestMessage>
  {

    public ShowRoleListRequestMessageValidator()
    {
    }
    
  }
}