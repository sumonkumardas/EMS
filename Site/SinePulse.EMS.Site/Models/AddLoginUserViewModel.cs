using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.RoleMessages;

namespace SinePulse.EMS.Site.Models
{
  public class AddLoginUserViewModel : BaseViewModel
  {
    public long EmployeeId { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
    public string RoleId { get; set; }
    public string InstituteId { get; set; }
    public string BranchId { get; set; }
    public string BranchMediumId { get; set; }
    public List<InstituteMessageModel> Institutes { get; set; } = new List<InstituteMessageModel>();
    public List<BranchMessageModel> Branches { get; set; } = new List<BranchMessageModel>();
    public List<BranchMediumMessageModel> BranchMediums { get; set; } = new List<BranchMediumMessageModel>();
    public ICollection<ShowRoleListResponseMessage.Role> UserRoles { get; set; } =
      new List<ShowRoleListResponseMessage.Role>();
  }
}