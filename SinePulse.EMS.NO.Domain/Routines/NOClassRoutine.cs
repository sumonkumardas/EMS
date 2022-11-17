using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Employees;
using SinePulse.EMS.NO.Domain.Shared;
using SinePulse.EMS.ProjectPrimitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Routines
{
  [Table("ClassRoutines")]
  [DisplayName("Class Routine")]
  public class NOClassRoutine : NOBaseEntity
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
    [Required, MemberOrder(10)]
    public virtual WeekDays WeekDay { get; set; }
    [Required, MemberOrder(20)]
    public virtual TimeSpan StartTime { get; set; }
    [Required, MemberOrder(30)]
    public virtual TimeSpan EndTime { get; set; }
    [Required, MemberOrder(40)]
    public virtual bool IsBreakTime { get; set; }
    [Required, MemberOrder(50)]
    public virtual StatusEnum Status { get; set; }
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
    [Optionally, MemberOrder(100)]
    [DisplayName("Subject")]
    public virtual NOSubject Subject { get; set; }
    [Optionally, MemberOrder(110)]
    [DisplayName("Teacher")]
    public virtual NOEmployee Teacher { get; set; }
    [Required, MemberOrder(120)]
    [DisplayName("SEction")]
    public virtual NOSection Section { get; set; }
    #endregion
  }
}
