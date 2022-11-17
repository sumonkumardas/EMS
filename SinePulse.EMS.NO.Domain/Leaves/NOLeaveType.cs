using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Leaves
{
  [Table("LeaveTypes")]
  [DisplayName("Leave Type")]
  [Bounded]
  public class NOLeaveType : NOBaseEntity
  {
    #region Injected Services
    public IDomainObjectContainer Container { set; protected get; }
    #endregion

    #region Life Cycle Methods
    public virtual void Persisting()
    {
      AuditFields.InsertedBy = Container.Principal.Identity.Name;
      AuditFields.InsertedDateTime = DateTime.Now;
      AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
      AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
    public virtual void Updating()
    {
      AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
      AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
    #endregion

    #region Primitive Properties
    [Required, StringLength(250), MemberOrder(10), Title]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string LeaveName { get; set; }
    [Required, MemberOrder(20)]
    public virtual int LeavesPerYear { get; set; }
    [MemberOrder(30)]
    public virtual bool CanEmployeesApplyBeyondTheCurrentLeaveBalance { get; set; }
    [MemberOrder(40)]
    public virtual bool WillLeaveCarriedForward { get; set; }
    [MemberOrder(50)]
    public virtual int PercentageOfLeaveCarriedForward { get; set; }
    [MemberOrder(60), Required]
    public virtual StatusEnum Status { get; set; }
    #endregion

    #region Complex Properties
    private AuditFields _auditFields = new AuditFields();
    [Required]
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
  }
}
