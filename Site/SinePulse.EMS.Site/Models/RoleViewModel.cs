using System.Collections.Generic;
using SinePulse.EMS.Messages.FeatureMessages;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class RoleViewModel : BaseViewModel
  {
    public string RoleId { get; set; }
    public RoleTypeEnum RoleType { get; set; }
    public string RoleName { get; set; }
    public List<GetFeaturesAndAssignedUsersOfRoleResponseMessage.Feature> Features { get; set; }
    public List<GetFeaturesAndAssignedUsersOfRoleResponseMessage.User> RoleUsers { get; set; }
  }
}