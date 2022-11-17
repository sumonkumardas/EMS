using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Employees;
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
  [Table("EmployeeLeaves")]
  [DisplayName("Employee Leave")]
  public class NOEmployeeLeave : NOBaseEntity
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

    public string Title()
    {
      var t = Container.NewTitleBuilder();

      string title = this.LeaveType.LeaveName;
      
      title = title + " -> " + this.StartDate.ToString() + " -> " + this.EndDate.ToString();
      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    [Required, MemberOrder(20)]
    public virtual DateTime StartDate { get; set; }
    [Required, MemberOrder(30)]
    public virtual DateTime EndDate { get; set; }
    [Required, StringLength(250), MemberOrder(50)]
    public virtual string Reason { get; set; }
    [Required, MemberOrder(40)]
    public virtual LeaveStatusEnum LeaveStatus { get; set; }
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
    [Required, MemberOrder(10)]
    public virtual NOLeaveType LeaveType { get; set; }
    [Required, MemberOrder(60)]
    public virtual NOEmployee Employee { get; set; }
    #endregion
  }
}
