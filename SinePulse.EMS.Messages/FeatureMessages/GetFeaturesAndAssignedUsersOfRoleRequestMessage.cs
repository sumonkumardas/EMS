using MediatR;

namespace SinePulse.EMS.Messages.FeatureMessages
{
  public class GetFeaturesAndAssignedUsersOfRoleRequestMessage : IRequest<GetFeaturesAndAssignedUsersOfRoleResponseMessage>
  {
    public string RoleId { get; set; }
  }
}