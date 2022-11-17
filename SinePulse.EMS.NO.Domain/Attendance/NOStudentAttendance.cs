using NakedObjects;
using SinePulse.EMS.NO.Domain.Shared;
using SinePulse.EMS.NO.Domain.Repositories;
using SinePulse.EMS.NO.Domain.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Attendance
{
  [Table("StudentAttendanceData")]
  [DisplayName("Student Attendance")]
  public class NOStudentAttendance : NOBaseEntity
  {
    #region Injected Services
    public IDomainObjectContainer Container { set; protected get; }
    public LoggedInUserInfoRepository LoggedInUserRepository { set; protected get; }
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
    [Mask("d")]
    public virtual DateTime AttendanceDate { get; set; }
    [Required, MemberOrder(20)]
    public virtual TimeSpan InTime { get; set; }
    [Required, MemberOrder(30)]
    public virtual TimeSpan OutTime { get; set; }
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

    #region  Navigation Properties
    [MemberOrder(100), Optionally, Disabled, Title]
    public virtual NOStudent Student { get; set; }
    #endregion
  }
}
