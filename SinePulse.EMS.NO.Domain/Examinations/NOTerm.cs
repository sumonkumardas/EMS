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
  [Table("ExamTerms")]
  [DisplayName("Exam Term")]
  [Bounded]
  public class NOTerm : NOBaseEntity
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
    [Required, StringLength(100), MemberOrder(10), Title]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string TermName { get; set; }
    [Required, MemberOrder(20)]
    [Mask("d")]
    public virtual DateTime StartDate { get; set; }
    [Required, MemberOrder(30)]
    [Mask("d")]
    public virtual DateTime EndDate { get; set; }
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
    public bool HideAuditFields()
    {
      return true;
    }
    #endregion

    #region  Navigation Properties
    public virtual BranchMedium Medium { set; get; }
    public virtual NOSession Session { set; get; }
    #endregion

    #region Exam Types (collection)
    private ICollection<NOExamType> _examTypes = new List<NOExamType>();
    [MemberOrder(150)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "Weight", "FullMarks", "IsSectionWiseExam")]
    public virtual ICollection<NOExamType> ExamTypes
    {
      get
      {
        return _examTypes;
      }
      set
      {
        _examTypes = value;
      }
    }
    #endregion

    #region Exam Routines (collection)
    private ICollection<NOExamRoutine> _examRoutines = new List<NOExamRoutine>();
    [MemberOrder(160)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(false, "ExamType", "TestDate", "StartTime", "EndTime")]
    public virtual ICollection<NOExamRoutine> ExamRoutines
    {
      get
      {
        return _examRoutines;
      }
      set
      {
        _examRoutines = value;
      }
    }
    #endregion

    #region Behavior
    public void AddExamType(NOClass noClass, string group, NOSubject subject, ExamTypeEnum type, decimal weight, decimal fullMarks, bool IsSectionWiseExam, [Optionally]string remarks)
    {
      GroupEnum groupEnum = (GroupEnum)Enum.Parse(typeof(GroupEnum), group);
      GroupSubject groupSubject = Container.Instances<GroupSubject>().Where(w => w.Class.Id == noClass.Id && w.Group == groupEnum && w.Subject.Id == subject.Id).FirstOrDefault();
      if(groupSubject != null)
      {
        NOExamType examType = Container.NewTransientInstance<NOExamType>();
        examType.Subject = groupSubject;
        examType.ExamType = type;
        examType.Remarks = remarks;
        examType.Weight = weight;
        examType.FullMarks = fullMarks;
        examType.Term = this;
        examType.Status = StatusEnum.Active;
        Container.Persist(ref examType);
      }
    }
    public string ValidateAddExamType(NOClass noClass, string group, NOSubject subject, ExamTypeEnum type, decimal weight, decimal fullMarks, bool IsSectionWiseExam, [Optionally]string remarks)
    {
      GroupEnum groupEnum = (GroupEnum)Enum.Parse(typeof(GroupEnum), group);
      GroupSubject groupSubject = Container.Instances<GroupSubject>().Where(w => w.Class.Id == noClass.Id && w.Group == groupEnum && w.Subject.Id == subject.Id).FirstOrDefault();

      if (groupSubject != null)
      {
        NOExamType examType = this.ExamTypes.Where(w => w.Subject.Id == groupSubject.Id && w.ExamType == type).FirstOrDefault();

        if (examType != null) return "YOu have already Added " + groupSubject.Title() + " -> " + type.ToString();

        decimal assignedWeight = this.ExamTypes.Where(w => w.Subject.Id == groupSubject.Id).Sum(s => s.Weight);

        if (weight > 1 - assignedWeight) return "Remain weight is " + (1 - assignedWeight).ToString();
      }

      return null;
    }
    public IList<NOClass> Choices0AddExamType()
    {
      return this.Medium.Classes.Where(w => w.Session.Id == this.Session.Id).Select(s => s.Class).Distinct().ToList();
    }
    public IList<string> Choices1AddExamType(NOClass noClass)
    {
      if (noClass == null) return new List<string>();
      IList<string> groups = new List<string>();
      IList<NOSection> sections = this.Medium.Classes.Where(w => w.Session.Id == this.Session.Id && w.Class.Id == noClass.Id).ToList();
      foreach (NOSection group in sections)
      {
        if (!groups.Contains(group.Group.ToString()))
          groups.Add(group.Group.ToString());
      }
      if (!groups.Contains("AllGroup"))
        groups.Add("AllGroup");
      return groups;
    }
    public IList<NOSubject> Choices2AddExamType(NOClass noClass, string group)
    {
      if (noClass == null || group == null) return new List<NOSubject>();
      GroupEnum groupEnum = (GroupEnum)Enum.Parse(typeof(GroupEnum), group);
      return Container.Instances<GroupSubject>().Where(w => w.Class.Id == noClass.Id && w.Group == groupEnum).Select(s => s.Subject).ToList();
    }

    public void AddExamRoutine(NOClass noClass, string group, NOSubject subject, string examTypeEnum, DateTime testTime, TimeSpan startTime, TimeSpan endTime)
    {
      GroupEnum groupEnum = (GroupEnum)Enum.Parse(typeof(GroupEnum), group);
      ExamTypeEnum typeEnum = (ExamTypeEnum)Enum.Parse(typeof(ExamTypeEnum), examTypeEnum);
      NOExamType examType = this.ExamTypes.Where(w => w.Subject.Class.Id == noClass.Id && w.Subject.Group == groupEnum && w.Subject.Subject.Id == subject.Id && w.ExamType== typeEnum).FirstOrDefault();
      if (examType != null)
      {
        NOExamRoutine examRoutine = Container.NewTransientInstance<NOExamRoutine>();
        examRoutine.ExamType = examType;
        examRoutine.StartTime = startTime;
        examRoutine.EndTime = endTime;
        examRoutine.TestDate = testTime;
        Container.Persist(ref examRoutine);
        ExamRoutines.Add(examRoutine);
      }
    }
    public IList<NOClass> Choices0AddExamRoutine()
    {
      return ExamTypes.Select(s => s.Subject.Class).Distinct().ToList();
    }
    public IList<string> Choices1AddExamRoutine(NOClass noClass)
    {
      if (noClass == null) return new List<string>();
      IList<string> groups = new List<string>();
      IList<GroupSubject> subjects = this.ExamTypes.Where(w => w.Subject.Class.Id == noClass.Id).Select(s => s.Subject).Distinct().ToList();
      foreach(GroupSubject group in subjects)
      {
        if (!groups.Contains(group.Group.ToString()))
          groups.Add(group.Group.ToString());
      }
      if (!groups.Contains("AllGroup"))
        groups.Add("AllGroup");
      return groups;
    }
    public IList<NOSubject> Choices2AddExamRoutine(NOClass noClass, string group)
    {
      if (noClass == null || group == null) return new List<NOSubject>();
      GroupEnum groupEnum = (GroupEnum)Enum.Parse(typeof(GroupEnum), group);
      return ExamTypes.Where(w => w.Subject.Class.Id == noClass.Id && w.Subject.Group == groupEnum).Select(s => s.Subject.Subject).Distinct().ToList();
    }
    public IList<string> Choices3AddExamRoutine(NOClass noClass, string group, NOSubject subject)
    {
      if (noClass == null || group == null || subject == null) return new List<string>();

      IList<string> examTypeEnum = new List<string>();
      examTypeEnum.Add("Subjective");
      examTypeEnum.Add("Objective");
      examTypeEnum.Add("Practical");
      GroupEnum groupEnum = (GroupEnum)Enum.Parse(typeof(GroupEnum), group);
      IList<NOExamType> examTypes = this.ExamRoutines.Where(w => w.ExamType.Subject.Class.Id == noClass.Id && w.ExamType.Subject.Group == groupEnum && w.ExamType.Subject.Subject.Id == subject.Id).Select(s=>s.ExamType).ToList();

      foreach(NOExamType examType in examTypes)
      {
        examTypeEnum.Remove(examType.ExamType.ToString());
      }
      return examTypeEnum;
    }
    #endregion
  }
}
