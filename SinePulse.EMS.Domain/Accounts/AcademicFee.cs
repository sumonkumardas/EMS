using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Accounts
{
  public class AcademicFee : BaseEntity
  {
    #region Primitive Properties

    public virtual decimal Fees { get; set; }
    public AcademicFeePeriodEnum FeePeriod { get; set; }

    #endregion

    #region Complex Properties

    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }

    #endregion

    #region  Navigation Properties

    public virtual Class Class { get; set; }
    public virtual BranchMediumAccountHead AccountHead { get; set; }

    #endregion
  }
}