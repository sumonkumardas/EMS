using NakedObjects;
using NakedObjects.Menu;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Employees;
using SinePulse.EMS.NO.Domain.Examinations;
using SinePulse.EMS.NO.Domain.Routines;
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

namespace SinePulse.EMS.NO.Domain.Academic
{
  [Table("Sections")]
  [DisplayName("Section")]
  [Bounded]
  public class NOSection : NOBaseEntity
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
        title = title + " -> Section (" + this.SectionName + ")";
      }
      else
      {
        title = title + " -> " + this.Group.ToString() + " -> Section (" + this.SectionName + ")";
      }
      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    [Required, MemberOrder(40)]
    public virtual GroupEnum Group { get; set; }
    public bool HideGroup()
    {
      if (this.Group == GroupEnum.AllGroup) return true;
      return false;
    }
    [Required, StringLength(50), MemberOrder(50)]
    public virtual string SectionName { get; set; }
    [Required, MemberOrder(60)]
    public virtual int NumberOfStudents { get; set; }
    [Required, MemberOrder(70)]
    public virtual int NumberOfPeriods { get; set; }
    [Required, MemberOrder(80)]
    public virtual int MaxPeriodDuration { get; set; }
    [Required, MemberOrder(100)]
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
    [Required, MemberOrder(10)]
    public virtual BranchMedium BranchMedium { get; set; }
    [Required, MemberOrder(20)]
    public virtual NOSession Session { get; set; }
    [Required, MemberOrder(30)]
    public virtual NOClass Class { get; set; }
    [Optionally, MemberOrder(40)]
    public virtual NORoom ClassRoom { get; set; }
    #endregion

    #region Class Tests (collection)
    private ICollection<NOClassTest> _classTests = new List<NOClassTest>();
    [MemberOrder(140)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "TestTime", "FullMarks")]
    public virtual ICollection<NOClassTest> ClassTests
    {
      get
      {
        return _classTests;
      }
      set
      {
        _classTests = value;
      }
    }
    #endregion

    #region Routines (collection)
    private ICollection<NOClassRoutine> _routines = new List<NOClassRoutine>();
    [MemberOrder(140)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "WeekDay", "StartTime", "EndTime", "Subject", "Teacher")]
    public virtual ICollection<NOClassRoutine> Routines
    {
      get
      {
        return _routines;
      }
      set
      {
        _routines = value;
      }
    }
    #endregion

    #region Behavior
    public void AssignOrChangeRoom(NORoom room)
    {
      this.ClassRoom = room;
    }
    public IList<NORoom> Choices0AssignOrChangeRoom()
    {
      IList<NORoom> rooms = Container.Instances<NOSection>().Where(w => w.BranchMedium.Id == this.BranchMedium.Id && w.Session.Id == this.Session.Id).Select(s=>s.ClassRoom).ToList();
      IList<long> roomIds = rooms.Select(s => s?.Id??0).ToList();

      return Container.Instances<NORoom>().Where(w => !roomIds.Contains(w.Id)).ToList();
    }
    public void AddClassRoutine(string day, TimeSpan startTime, TimeSpan endTime, NOSubject subject, NOEmployee teacher)
    {
      WeekDays dayEnum = (WeekDays)Enum.Parse(typeof(WeekDays), day);  //WeekDays enumByValue = (WeekDays) 64;
      NOClassRoutine routine = Container.NewTransientInstance<NOClassRoutine>();
      routine.WeekDay = dayEnum;
      routine.StartTime = startTime;
      routine.EndTime = endTime;
      routine.Subject = subject;
      routine.Teacher = teacher;
      routine.IsBreakTime = false;
      routine.Section = this;
      Container.Persist(ref routine);
    }
    public IList<string> Choices0AddClassRoutine()
    {
      IList<string> dayList = new List<string>();
      foreach (string str in Enum.GetNames(typeof(WeekDays)))
      {
        string holidays = this.BranchMedium.WeeklyHolidays.ToString();
        if (str != WeekDays.None.ToString())
        {
          if (!holidays.Contains(str))
          {
            dayList.Add(str);
          }
        }
      }
      return dayList;
    }
    public IList<NOSubject> Choices3AddClassRoutine()
    {
      return this.Class.Subjects.Where(w => w.Group == this.Group || w.Group == GroupEnum.AllGroup).Select(s => s.Subject).ToList();
    }
    public IList<NOEmployee> Choices4AddClassRoutine()
    {
      return Container.Instances<NOEmployee>().Where(w => w.EmployeeType == EmployeeTypeEnum.Teacher &&
                                                     w.Branch.Id == this.BranchMedium.Id).ToList();
    }

    public void AddClassTest(NOTerm examTerm, NOExamType examType, DateTime testDate,  decimal fullMarks, [Optionally]string remarks)
    {
      NOClassTest classTest = Container.NewTransientInstance<NOClassTest>();
      classTest.ExamTypeEnum = examType.ExamType;
      classTest.ExamType = examType;
      classTest.Remarks = remarks;
      classTest.TestTime = testDate;
      classTest.FullMarks = fullMarks;
      classTest.Section = this;
      classTest.Status = StatusEnum.Active;
      Container.Persist(ref classTest);
    }
    public IList<NOTerm> Choices0AddClassTest()
    {
      return this.BranchMedium.ExamTerms.Where(w=>w.Session.Id == this.Session.Id).ToList();
    }
    public IList<NOExamType> Choices1AddClassTest(NOTerm examTerm)
    {
      if (examTerm != null)
        return examTerm.ExamTypes.Where(w => w.Term.Id == examTerm.Id && w.Subject.Class.Id == this.Class.Id && (w.Subject.Group== this.Group || w.Subject.Group == GroupEnum.AllGroup) && w.ExamType != ExamTypeEnum.ClassTest).ToList();

      return new List<NOExamType>();
    }
    #endregion

    public static void Menu(IMenu menu)
    {
      menu.AddAction("AssignOrChangeRoom");
      menu.AddAction("AddClassRoutine");
      menu.AddAction("AddClassTest");
    }
  }
}
