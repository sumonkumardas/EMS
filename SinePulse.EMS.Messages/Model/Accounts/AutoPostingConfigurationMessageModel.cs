

using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Accounts
{
  public class AutoPostingConfigurationMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public virtual VoucherTypeEnum VoucherType { get; set; }
    public virtual string AccountCode { get; set; }
    #endregion
  }
}
