using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace SinePulse.EMS.Domain.Employees
{
    public class EducationalQualification : BaseEntity
  {
    #region Primitive Properties
    public virtual string InstituteName { get; set; }
    public virtual EducationDegreeEnum DegreeName { get; set; }
    public virtual string MajorSubject { get; set; }
    public virtual string PassingYear { get; set; }
    #endregion

    #region Complex Properties
    private AuditFields _auditFields = new AuditFields();
    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }
    public bool HideAuditFields()
    {
      return true;
    }
    #endregion

    #region  Navigation Properties
    [Required]
    public virtual Employee Employee { get; set; }
    #endregion
  }
}
