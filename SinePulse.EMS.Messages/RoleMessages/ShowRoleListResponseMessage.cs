using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Messages.RoleMessages
{
  public class ShowRoleListResponseMessage
  {
    public ShowRoleListResponseMessage(ICollection<Role> roles)
    {
      Roles = roles;
    }

    public ICollection<Role> Roles { get; }
    
    
    public class Role
    {
      public RoleTypeEnum RoleType { get; set; }
      public string RoleId { get; set; }
      public string RoleName { get; set; }
      public StatusEnum Status { get; set; }
    }
  }
}