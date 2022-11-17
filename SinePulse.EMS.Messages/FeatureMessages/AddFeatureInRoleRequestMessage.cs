using MediatR;

namespace SinePulse.EMS.Messages.FeatureMessages
{
  public class AddFeatureInRoleRequestMessage : IRequest<AddFeatureInRoleResponseMessage>
  {
    public string RoleId { get; set; }
    public long[] FeatureIds { get; set; }
  }
}