using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Banks
{
  public class BankMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string BankName { get; set; }
    #endregion

    #region Navigation Properties
    public BranchMediumMessageModel BranchMedium { get; set; }
    #endregion
    
    #region Collection Properties
    public ICollection<BankBranchMessageModel> BankBranches { get; set; }
    #endregion
  }
}