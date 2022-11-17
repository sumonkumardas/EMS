using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class BranchMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string BranchName { get; set; }
    public string BranchCode { get; set; }
    public string MobileNo { get; set; }
    public string Email { get; set; }
    public string Fax { get; set; }
    public StatusEnum Status { get; set; }
    #endregion

    #region  Navigation Properties

    public InstituteMessageModel Institute { get; set; }
    public AddressMessageModel Address { get; set; }
    public ICollection<BranchMediumMessageModel> Mediums { get; set; } = new List<BranchMediumMessageModel>();
    public ICollection<SessionMessageModel> Sessions { get; set; } = new List<SessionMessageModel>();

    #endregion
  }
}
