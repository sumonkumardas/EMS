using MediatR;

namespace SinePulse.EMS.Messages.FeatureMessages
{
  public class GetFeaturesInRoleRequestMessage : IRequest<GetFeaturesInRoleResponseMessage>
  {
    public string RoleId { get; set; }
    public long FeatureTypeId { get; set; }
  }
}