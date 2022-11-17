using MediatR;

namespace SinePulse.EMS.Messages.FeatureMessages
{
  public class GetFeaturesToAddInRoleRequestMessage : IRequest<GetFeaturesToAddInRoleResponseMessage>
  {
    public long FeatureTypeId { get; set; }
    public string RoleId { get; set; }
  }
}