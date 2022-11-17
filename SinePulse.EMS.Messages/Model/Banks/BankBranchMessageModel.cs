using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Banks
{
  public class BankBranchMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string BranchName { get; set; }
    public string Address { get; set; }

    #endregion

    #region Navigation Properties

    public BankMessageModel Bank { get; set; }

    #endregion

    #region Collection Properties

    public ICollection<BankAccountMessageModel> BankAccounts { get; set; }

    #endregion
  }
}