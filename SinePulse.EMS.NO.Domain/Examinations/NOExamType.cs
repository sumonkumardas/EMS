using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Shared;

namespace SinePulse.EMS.NO.Domain.Examinations
{
  [Table("ExamTypes")]
  [DisplayName("Exam Type")]
  [Bounded]
  public class NOExamType: NOBaseEntity
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

      string title = this.Subject.Title() + " -> " + this.ExamType.ToString();

      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    //[Required, StringLength(100), MemberOrder(10), Title]
    //[RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    //public virtual string ExamTypeName { get; set; }
    [Required, MemberOrder(10)]
    public virtual ExamTypeEnum ExamType { get; set; }
    [Optionally, StringLength(100), MemberOrder(60)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string Remarks { get; set; }
    [Required, MemberOrder(30)]
    public virtual decimal Weight { get; set; }
    [Required, MemberOrder(40)]
    public virtual decimal FullMarks { get; set; }
    [Required, MemberOrder(70)]
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
    public virtual NOTerm Term { get; set; }
    public virtual GroupSubject Subject { set; get; }
    #endregion
  }
}
