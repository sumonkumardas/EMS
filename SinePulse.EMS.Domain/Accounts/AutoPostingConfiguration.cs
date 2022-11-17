using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Accounts
{
  public class AutoPostingConfiguration : BaseEntity
  {
    #region Primitive Properties

    public virtual VoucherTypeEnum VoucherType { get; set; }
    public virtual string AccountCode { get; set; }

    #endregion

    #region Complex Properties

    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get => _auditFields;
      set => _auditFields = value;
    }

    #endregion
  }
}