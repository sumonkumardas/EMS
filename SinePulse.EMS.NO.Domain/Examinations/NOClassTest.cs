using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Examinations
{
  [Table("ClassTests")]
  [DisplayName("Class Test")]
  public class NOClassTest: NOBaseEntity
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
    public virtual ExamTypeEnum ExamTypeEnum { get; set; }
    [Required, MemberOrder(30)]
    [Mask("d")]
    public virtual DateTime TestTime { get; set; }
    [Required, MemberOrder(40)]
    public virtual decimal Weight { get; set; }
    [Required, MemberOrder(50)]
    public virtual decimal FullMarks { get; set; }
    [Optionally, StringLength(100), MemberOrder(20)]
    public virtual string Remarks { get; set; }

    [Required, MemberOrder(60)]
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
    [Title]
    public virtual NOExamType ExamType { get; set; }
    public virtual NOSection Section { get; set; }
    #endregion
  }
}
