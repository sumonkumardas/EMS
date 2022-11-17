using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class ShowRoleListViewModel : BaseViewModel
  {
    public ICollection<Role> Roles { get; set; }
    
    public class Role
    {
      public string RoleId { get; set; }
      public string RoleName { get; set; }
      public StatusEnum Status { get; set; }
      public RoleTypeEnum RoleType { get; set; }
    }
  }
}