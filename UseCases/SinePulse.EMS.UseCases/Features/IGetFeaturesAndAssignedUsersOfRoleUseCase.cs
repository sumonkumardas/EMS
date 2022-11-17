using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public interface IGetFeaturesAndAssignedUsersOfRoleUseCase
  {
    GetFeaturesAndAssignedUsersOfRoleResponseMessage.FeaturesAndUsers GetFeaturesAndAssignedUsersOfRole(
      GetFeaturesAndAssignedUsersOfRoleRequestMessage message);
  }
}