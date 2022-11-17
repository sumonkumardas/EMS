using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Messages.RoleMessages
{
  public class ShowRoleResponseMessage
  {
    public Role RoleData { get; }

    public ShowRoleResponseMessage(Role roleData)
    {
      RoleData = roleData;
    }

    public class Role
    {
      public RoleTypeEnum RoleType { get; set; }
      public string RoleId { get; set; }
      public string RoleName { get; set; }
      public StatusEnum Status { get; set; }
    }
  }
}