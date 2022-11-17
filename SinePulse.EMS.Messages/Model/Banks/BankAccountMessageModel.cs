using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Banks
{
  public class BankAccountMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public virtual string AccountNumber { get; set; }
    public virtual BankAccountTypeEnum AccountType { get; set; }

    #endregion

    #region Navigation Properties

    public virtual BankBranchMessageModel BankBranch { get; set; }

    #endregion
  }
}