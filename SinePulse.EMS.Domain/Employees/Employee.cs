using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.UserManagement;

namespace SinePulse.EMS.Domain.Employees
{
  public class Employee : BaseEntity
  {
    public override string Title()
    {
      return this.FullName + " (" + this.MobileNo + ")";
    }

    #region Primitive Properties

    public virtual string FullName { get; set; }
    public virtual string EmployeeCode { get; set; }
    public virtual DateTime? DOB { get; set; }
    public virtual string NationalId { get; set; }
    public virtual string MobileNo { get; set; }
    public virtual string EmailAddress { get; set; }
    public virtual DateTime? JoiningDate { get; set; }
    public virtual string BankAccountNo { get; set; }
    public virtual EmployeeTypeEnum EmployeeType { get; set; }
    public virtual StatusEnum Status { get; set; }
    public virtual AssociationType AssociatedWith { get; set; }
    public virtual long? ReportToId { get; set; }
    public virtual string ImageUrl { get; set; }

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
    public virtual Institute Institute { get; set; }
    public virtual Branch Branch { get; set; }
    public virtual BranchMedium BranchMedium { get; set; }
    public virtual Designation Designation { get; set; }
    public virtual Grade Grade { get; set; }
    public virtual JobType JobType { get; set; }
    public virtual Address PresentAddress { get; set; }
    public virtual Address PermanentAddress { get; set; }
    public virtual Employee ReportTo { get; set; }
    public virtual LoginUser LoginUser { get; set; }
    #endregion

    #region EducationalQualifications (collection)

    public virtual ICollection<EducationalQualification> EducationalQualifications { get; set; } =
      new List<EducationalQualification>();

    #endregion
  }
}