using MediatR;

namespace SinePulse.EMS.Messages.FeatureMessages
{
  public class RemoveFeatureFromRoleRequestMessage : IRequest<RemoveFeatureFromRoleResponseMessage>
  {
    public string RoleId { get; set; }
    public long[] FeatureIds { get; set; }
  }
}