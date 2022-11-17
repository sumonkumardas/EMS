
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Transactions
{
  public class TransactionEntryMessageModel : BaseEntityMessageModel
    {

    #region Primitive Properties

    public  decimal Amount { get; set; }

    #endregion

    #region  Navigation Properties

    public TransactionMessageModel Transaction { get; set; }
    public BranchMediumAccountsHeadMessageModel AccountHead { get; set; }

    #endregion
  }
}