using FluentValidation;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public class GetFeaturesAndAssignedUsersOfRoleRequestMessageValidator : AbstractValidator<
    GetFeaturesAndAssignedUsersOfRoleRequestMessage>
  {
    public GetFeaturesAndAssignedUsersOfRoleRequestMessageValidator()
    {
    }
  }
}