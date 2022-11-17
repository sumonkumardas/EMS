using NakedObjects;
using NakedObjects.Menu;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Accounts;
using SinePulse.EMS.NO.Domain.Designations;
using SinePulse.EMS.NO.Domain.Employees;
using SinePulse.EMS.NO.Domain.Examinations;
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
using SinePulse.EMS.NO.Domain.Banks;

namespace SinePulse.EMS.NO.Domain.Academic
{
  [DisplayName("Medium")]
  public class BranchMedium : NOBaseEntity
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

      string title = this.Shift.ShiftName + " -> " + this.Medium.MediumName;

      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    [MemberOrder(10), Required]
    public virtual WeekDays WeeklyHolidays { get; set; }
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
    [Required, MemberOrder(100)]
    public virtual NOBranch Branch { get; set; }
    [Required, MemberOrder(120)]
    public virtual NOShift Shift { get; set; }
    [Required, MemberOrder(130)]
    public virtual NOMedium Medium { get; set; }
    #endregion

    #region Sessions (collection)
    private ICollection<NOSession> _sessions = new List<NOSession>();
    [MemberOrder(150), NakedObjectsIgnore]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "StartDate", "EndDate", "IsSessionClosed")]
    public virtual ICollection<NOSession> MySessions
    {
      get
      {
        return _sessions;
      }
      set
      {
        _sessions = value;
      }
    }
    #endregion

    #region Rooms (collection)
    private ICollection<NORoom> _rooms = new List<NORoom>();
    [MemberOrder(160)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "RoomName", "SeatCapacity", "ExamSeatCapacity")]
    public virtual ICollection<NORoom> Rooms
    {
      get
      {
        return _rooms;
      }
      set
      {
        _rooms = value;
      }
    }
    #endregion

    #region Classes (collection)
    private ICollection<NOSection> _classes = new List<NOSection>();
    [MemberOrder(170)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "Session", "NumberOfStudents")]
    public virtual ICollection<NOSection> Classes
    {
      get
      {
        return _classes;
      }
      set
      {
        _classes = value;
      }
    }
    #endregion

    #region BreakTimes (collection)
    private ICollection<NOBreakTime> _breakTimes = new List<NOBreakTime>();
    [MemberOrder(180)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "WeekDays", "StartTime", "EndTime", "Session")]
    public virtual ICollection<NOBreakTime> BreakTimes
    {
      get
      {
        return _breakTimes;
      }
      set
      {
        _breakTimes = value;
      }
    }
    #endregion

    #region Exam Terms (collection)
    private ICollection<NOTerm> _examTerms = new List<NOTerm>();
    [MemberOrder(190)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "Session", "StartDate", "EndDate")]
    public virtual ICollection<NOTerm> ExamTerms
    {
      get
      {
        return _examTerms;
      }
      set
      {
        _examTerms = value;
      }
    }
    #endregion

    #region Get Properties
    [NotMapped, MemberOrder(200)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "BankName", "BranchName")]
    public IList<NOBankInfo> Banks
    {
      get
      {
        return Container.Instances<NOBankInfo>().Where(w => w.BranchMedium.Id == this.Id).ToList();
      }
    }
    [NotMapped, MemberOrder(500)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "MobileNo", "JoiningDate", "EmployeeType", "Designation", "Grade", "ReportTo")]
    public virtual IList<NOEmployee> Employees
    {
      get
      {
        return Container.Instances<NOEmployee>().Where(w => w.Branch.Id == this.Id).ToList();
      }
    }

    [NotMapped, MemberOrder(600)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "StartDate", "EndDate", "IsSessionClosed")]
    public virtual IList<NOSession> Sessions
    {
      get
      {
        if (this.MySessions.Any()) return this.MySessions.ToList();

        if (this.Branch.Sessions.Any()) return this.Branch.Sessions.ToList();

        if (this.Branch.Institute.Sessions.Any()) return this.Branch.Institute.Sessions.ToList();

        return Container.Instances<NOSession>().Where(w => w.SessionType == ObjectTypeEnum.Global).ToList();
      }
    }
    #endregion

    #region Behavior
    public void AddSession(string sessionName, DateTime startDate, DateTime endDate, [Optionally] IEnumerable<SassionwiseDataEnum> dataNeedToCopy)
    {
      NOSession session = Container.NewTransientInstance<NOSession>();
      session.SessionName = sessionName;
      session.StartDate = startDate;
      session.EndDate = endDate;
      session.IsSessionClosed = false;
      session.SessionType = ObjectTypeEnum.Medium;
      session.Status = StatusEnum.Active;
      Container.Persist(ref session);
      this.MySessions.Add(session);
    }

    public void AddClassBreakTime (NOSession session, IEnumerable<WeekDays> weekDays, TimeSpan startTime, TimeSpan endTime)
    {
      WeekDays weekDay = WeekDays.None;
      foreach (WeekDays day in weekDays)
      {
        if (weekDay == WeekDays.None)
        {
          weekDay = day;
        }
        else
        {
          weekDay |= day;
        }
      }

      NOBreakTime breakTime = Container.NewTransientInstance<NOBreakTime>();
      breakTime.WeekDays = weekDay;
      breakTime.StartTime = startTime;
      breakTime.EndTime = endTime;
      breakTime.Session = session;
      breakTime.Branch = this;
      breakTime.Status = StatusEnum.Active;
      Container.Persist(ref breakTime);
    }
    public IList<NOSession> Choices0AddClassBreakTime()
    {
      return GetSessions();
    }
    public void AddSection(NOSession session, NOClass noClass, GroupEnum group, string sectionName, int numberOfStudents, int numberOfPeriods, int maxClassDuration)
    {
      NOSection section = Container.NewTransientInstance<NOSection>();
      section.SectionName = sectionName;
      section.Group = group;
      section.NumberOfStudents = numberOfStudents;
      section.NumberOfPeriods = numberOfPeriods;
      section.MaxPeriodDuration = maxClassDuration;
      section.Class = noClass;
      section.Session = session;
      section.BranchMedium = this;
      section.Status = StatusEnum.Active;
      Container.Persist(ref section);
      Classes.Add(section);
    }
    public IList<NOSession> Choices0AddSection()
    {
      return GetSessions();
    }
    
    public void AddExamTerm(NOSession session, string termName, DateTime startDate, DateTime endDate)
    {
      NOTerm examTerm = Container.NewTransientInstance<NOTerm>();
      examTerm.TermName = termName;
      examTerm.StartDate = startDate;
      examTerm.EndDate = endDate;
      examTerm.Medium = this;
      examTerm.Session = session;
      examTerm.Status = StatusEnum.Active;
      Container.Persist(ref examTerm);
    }
    public IList<NOSession> Choices0AddExamTerm()
    {
      return GetSessions();
    }
    private IList<NOSession> GetSessions()
    {
      if (this.Sessions.Any()) return this.Sessions.ToList();

      if (this.Branch.Sessions.Any()) return this.Branch.Sessions.ToList();

      if (this.Branch.Institute.Sessions.Any()) return this.Branch.Institute.Sessions.ToList();

      return Container.Instances<NOSession>().Where(w => w.SessionType == ObjectTypeEnum.Global).ToList();
    }
    public NOEmployee AddEmployee([Optionally]NOEmployee reportTo, string fullName, DateTime dOB, string nationalId, string mobileNo, string emailAddress, DateTime joiningDate, EmployeeTypeEnum employeeType,
                                 NODesignation designation, NOJobType jobType, NOGrade grade)
    {
      NOEmployee employee = Container.NewTransientInstance<NOEmployee>();
      employee.FullName = fullName;
      employee.DOB = dOB;
      employee.NationalId = nationalId;
      employee.MobileNo = mobileNo;
      employee.EmailAddress = emailAddress;
      employee.JoiningDate = joiningDate;
      employee.EmployeeType = employeeType;
      employee.ReportTo = reportTo;
      employee.Designation = designation;
      employee.JobType = jobType;
      employee.Grade = grade;
      employee.Status = StatusEnum.Active;
      employee.Branch = this;
      Container.Persist(ref employee);
      return employee;
    }

    public IList<NOEmployee> Choices0AddEmployee()
    {
      return Container.Instances<NOEmployee>().Where(w => w.EmployeeType == EmployeeTypeEnum.Teacher
                                                     && w.Branch.Id == this.Id).ToList();
    }
    //[PageSize(10)]
    //RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Employee Name")
    //public IQueryable<NOEmployee> AutoComplete0AddEmployee(
    //  [MinLength(1)]
    //  string employeeName)
    //{
    //  return Container.Instances<NOEmployee>().Where(w => w.FullName.Contains(employeeName) 
    //                                                 && w.EmployeeType == EmployeeTypeEnum.Teacher
    //                                                 && w.Branch.Id == this.Id);
    //}
    public void SetAcademicFees(NOSession session, NOClass noClass, AccountPeriodEnum accountPeriod, NOAccountHead accountHead, decimal fee)
    {
      NOAcademicFee academicFee = Container.NewTransientInstance<NOAcademicFee>();
      academicFee.Fees = fee;
      academicFee.Session = session;
      academicFee.Class = noClass;
      academicFee.AccountHead = accountHead;
      academicFee.BranchMedium = this;
      Container.Persist(ref academicFee);
    }
    public IList<NOSession> Choices0SetAcademicFees()
    {
      return GetSessions();
    }
    public IList<NOClass> Choices1SetAcademicFees()
    {
      return this.Classes.Select(s => s.Class).ToList();
    }
    public IList<NOAccountHead> Choices3SetAcademicFees(AccountPeriodEnum accountPeriod)
    {
      return Container.Instances<NOAccountHead>().Where(w => w.AccountHeadType == AccountHeadTypeEnum.Academic && w.AccountPeriod == accountPeriod).ToList();
    }
    public void ShowAcademicCalendar()
    {

    }
    public void ImportData (ObjectTypeEnum objectType, NOSession session, IEnumerable<SassionwiseDataEnum> dataType)
    {

    }
    public IList<NOSession> Choices1ImportData(ObjectTypeEnum objectType)
    {
      return Container.Instances<NOSession>().Where(w => w.SessionType == objectType).ToList();
    }
    public void AddBankInfo(string bankName, string branchName, string accountNumber)
    {
      NOBankInfo bankInfo = Container.NewTransientInstance<NOBankInfo>();
      bankInfo.BankName = bankName;
      bankInfo.BranchName = branchName;
      bankInfo.AccountNumber = accountNumber;
      bankInfo.BranchMedium = this;
      Container.Persist(ref bankInfo);

      NOAccountHead bankParentAccountHead = Container.Instances<NOAutoPostingConfig>().Where(w => w.VoucherType == VoucherTypeEnum.BankVoucher).Select(s => s.AccountHead).FirstOrDefault();
      if(bankParentAccountHead != null)
      {
        int maxNumber = bankParentAccountHead.Childs.Count + 1;

        NOAccountHead accountHead = Container.NewTransientInstance<NOAccountHead>();
        accountHead.AccountCode = bankParentAccountHead.AccountCode.Substring(0,3) + maxNumber.ToString();
        accountHead.AccountHeadName = bankInfo.AccountNumber; ;
        accountHead.ParentHead = bankParentAccountHead;
        accountHead.AccountHeadType = AccountHeadTypeEnum.Bank ;
        accountHead.AccountType = bankParentAccountHead.AccountType;
        accountHead.AccountPeriod =  AccountPeriodEnum.None;
        accountHead.IsLedger = true;
        accountHead.Status = StatusEnum.Active;
        Container.Persist(ref accountHead);

        NOBankAccountHead bankAccountHead = Container.NewTransientInstance<NOBankAccountHead>();
        bankAccountHead.Bank = bankInfo;
        bankAccountHead.AccountHead = accountHead;
        Container.Persist(ref bankAccountHead);
      }
    }
    #endregion

    public static void Menu(IMenu menu)
    {
      menu.AddAction("AddSession");
      menu.AddAction("AddBankInfo");
      menu.AddAction("AddClassBreakTime");
      menu.AddAction("AddSection");
      menu.AddAction("AddExamTerm");
      menu.AddAction("ShowAcademicCalendar");
      menu.AddAction("AddEmployee");
      menu.AddAction("SetAcademicFees");
      menu.AddAction("ImportData");
    }
  }
}
