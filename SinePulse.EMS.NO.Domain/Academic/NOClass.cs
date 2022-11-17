﻿using NakedObjects;
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

namespace SinePulse.EMS.NO.Domain.Academic
{
  [Table("Classes")]
  [DisplayName("Class")]
  [Bounded]
  public class NOClass : NOBaseEntity
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
    [Required, MemberOrder(10), Title]
    public virtual string ClassName { get; set; }
    [Required, MemberOrder(20)]
    public virtual string ClassCode { get; set; }
    [Required, MemberOrder(30)]
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

    #region Get Properties
    [NotMapped, MemberOrder(500)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(false, "Group", "Subject", "FullMarks", "PassMarks")]
    public IList<GroupSubject> Subjects
    {
      get
      {
        return Container.Instances<GroupSubject>().Where(w => w.Class.Id == this.Id).ToList();
      }
    }
    #endregion

    #region Behavior
    public void AddSubject (GroupEnum group, IEnumerable<NOSubject> subjects, int fullMark, int passMark)
    {
      foreach (NOSubject subject in subjects)
      {
        GroupSubject groupwiseSubject = Container.NewTransientInstance<GroupSubject>();
        groupwiseSubject.Class = this;
        groupwiseSubject.Group = group;
        groupwiseSubject.FullMarks = fullMark;
        groupwiseSubject.PassMarks = passMark;
        groupwiseSubject.Subject = subject;
        groupwiseSubject.Status = StatusEnum.Active;
        Container.Persist(ref groupwiseSubject);
      }
    }

    #endregion
  }
}
