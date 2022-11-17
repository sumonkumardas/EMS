using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class AddRoleViewModel : BaseViewModel
  {
    public string RoleName { get; set; }
    public RoleTypeEnum RoleType { get; set; }
  }
}