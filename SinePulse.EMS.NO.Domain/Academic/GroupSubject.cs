using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Academic
{
  [Bounded]
  public class GroupSubject : NOBaseEntity
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

      string title = this.Class.ClassName;

      if (this.Group == GroupEnum.AllGroup)
      {
        title = title + " -> " + this.Subject.SubjectName;
      }
      else
      {
        title = title + " -> " + this.Group.ToString() + " -> " + this.Subject.SubjectName;
      }

      t.Append(title);

      return t.ToString();
    }
    #region Primitive Properties
    [Required, MemberOrder(10)]
    public virtual GroupEnum Group { get; set; }
    [Required, MemberOrder(20)]
    public virtual int FullMarks { get; set; }
    [Required, MemberOrder(30)]
    public virtual int PassMarks { get; set; }
    [Required, MemberOrder(40)]
    public virtual StatusEnum Status { get; set; }
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
    [Required, MemberOrder(110)]
    public virtual NOClass Class { get; set; }
    [Required, MemberOrder(100)]
    public virtual NOSubject Subject { get; set; }
    #endregion
  }
}
