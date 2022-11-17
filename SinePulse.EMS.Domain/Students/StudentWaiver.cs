using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Students
{
  public class StudentWaiver : BaseEntity
  {
    #region Primitive Propertise

    public decimal Fees { get; set; }
    public decimal Waiver { get; set; }
    public bool InPercentage { get; set; }

    #endregion

    #region Complex Properties

    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }

    #endregion

    #region Navigation Properties

    public Student Student { get; set; }
    public Section Section { get; set; }
    public BranchMediumAccountHead AccountHead { get; set; }

    #endregion
  }
}