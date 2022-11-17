using NakedObjects;
using NakedObjects.Menu;
using NakedObjects.Services;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Designations;
using SinePulse.EMS.NO.Domain.Employees;
using SinePulse.EMS.NO.Domain.Holidays;
using SinePulse.EMS.NO.Domain.Leaves;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Repositories
{
  [DisplayName("Setup")]
  public class SetupRepository : AbstractFactoryAndRepository
  {
    public static void Menu(IMenu menu)
    {
      IMenu academicMenu = menu.CreateSubMenu("Academic");

      //IMenu sessionMenu = academicMenu.CreateSubMenu("Session");
      //sessionMenu.AddAction("NewSession");
      //sessionMenu.AddAction("ShowAllSession");

      //IMenu shiftMenu = academicMenu.CreateSubMenu("Shift");
      //shiftMenu.AddAction("NewShift");
      //shiftMenu.AddAction("ShowAllShift");

      IMenu mediumMenu = academicMenu.CreateSubMenu("Medium");
      mediumMenu.AddAction("NewMedium");
      mediumMenu.AddAction("ShowAllMedium");

      IMenu subjectMenu = academicMenu.CreateSubMenu("Subject");
      subjectMenu.AddAction("NewSubject");
      subjectMenu.AddAction("ShowAllSubjects");

      IMenu classMenu = academicMenu.CreateSubMenu("Class");
      classMenu.AddAction("NewClass");
      classMenu.AddAction("ShowAllClasses");

      IMenu employeeMenu = menu.CreateSubMenu("Employee");

      IMenu designationMenu = employeeMenu.CreateSubMenu("Designtation");
      designationMenu.AddAction("NewDesignation");
      designationMenu.AddAction("ShowAllDesigntions");

      IMenu jobTypeMenu = employeeMenu.CreateSubMenu("Job Type");
      jobTypeMenu.AddAction("NewJobType");
      jobTypeMenu.AddAction("ShowAllJpbTypes");

      IMenu gradeMenu = employeeMenu.CreateSubMenu("Grade");
      gradeMenu.AddAction("NewGrade");
      gradeMenu.AddAction("ShowAllGrades");

      IMenu leaveMenu = menu.CreateSubMenu("Leave");
      IMenu leaveTypeMenu = leaveMenu.CreateSubMenu("Leave Type");
      leaveTypeMenu.AddAction("NewLeaveType");
      leaveTypeMenu.AddAction("ShowAllLeaveTypes");

      IMenu holidayMenu = menu.CreateSubMenu("Holiday");
      holidayMenu.AddAction("NewPublicHoliday");
      holidayMenu.AddAction("ShowAllHolidays");
    }

    #region Session
    public NOSession NewSession(string sessionName, DateTime startDate, DateTime endDate, [Optionally] IEnumerable<SassionwiseDataEnum> dataNeedToCopy)
    {
      NOSession session = Container.NewTransientInstance<NOSession>();
      session.SessionName = sessionName;
      session.StartDate = startDate;
      session.EndDate = endDate;
      session.IsSessionClosed = false;
      session.SessionType = ObjectTypeEnum.Global;
      session.Status = StatusEnum.Active;
      Container.Persist(ref session);
      return session;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "SessionName", "StartDate", "EndDate", "IsSessionClosed", "SessionType")]
    public IList<NOSession> ShowAllSession()
    {
      return Container.Instances<NOSession>().ToList();
    }
    #endregion

    #region Shift
    public NOShift NewShift(string shiftName, TimeSpan startTime, TimeSpan endTime)
    {
      NOShift shift = Container.NewTransientInstance<NOShift>();
      shift.ShiftName = shiftName;
      shift.StartTime = startTime;
      shift.EndTime = endTime;
      shift.ShiftType = ObjectTypeEnum.Global;
      shift.Status = StatusEnum.Active;
      Container.Persist(ref shift);
      return shift;
    }
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "ShiftName", "StartTime", "EndTime", "Status")]
    public IList<NOShift> ShowAllShift()
    {
      return Container.Instances<NOShift>().ToList();
    }
    #endregion

    #region Medium
    public NOMedium NewMedium(string mediumName, string mediumCode)
    {
      NOMedium medium = Container.NewTransientInstance<NOMedium>();
      medium.MediumName = mediumName;
      medium.MediumCode = mediumCode;
      medium.Status = StatusEnum.Active;
      Container.Persist(ref medium);
      return medium;
    }
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "MediumName", "Medium Code", "Status")]
    public IList<NOMedium> ShowAllMedium()
    {
      return Container.Instances<NOMedium>().ToList();
    }
    #endregion

    #region Class
    public NOClass NewClass(string className, string classCode)
    {
      NOClass noClass = Container.NewTransientInstance<NOClass>();
      noClass.ClassName = className;
      noClass.ClassCode = classCode;
      noClass.Status = StatusEnum.Active;
      Container.Persist(ref noClass);
      return noClass;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "ClassName", "ClassCode", "Status")]
    public IList<NOClass> ShowAllClasses()
    {
      return Container.Instances<NOClass>().ToList();
    }
    #endregion

    #region Subject
    public NOSubject NewSubject(string subjectName, string subjectCode, int fullMarks, ResultTypeEnum resultType, [Optionally] NOSubject combinedWith, bool isCombinedPass, bool isOptional)
    {
      NOSubject subject = Container.NewTransientInstance<NOSubject>();
      subject.SubjectName = subjectName;
      subject.SubjectCode = subjectCode;
      subject.FullMarks = fullMarks;
      subject.ResultType = resultType;
      subject.CombinedWith = combinedWith;
      subject.IsCombinedPass = isCombinedPass;
      subject.IsOptional = isOptional;
      subject.Status = StatusEnum.Active;
      Container.Persist(ref subject);
      return subject;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "SubjectName", "SubjectCode", "FullMarks", "ResultType")]
    public IList<NOSubject> ShowAllSubjects()
    {
      return Container.Instances<NOSubject>().ToList();
    }
    #endregion

    #region Designation
    public NODesignation NewDesignation(string title, EmployeeTypeEnum employeeType)
    {
      NODesignation designation = Container.NewTransientInstance<NODesignation>();

      designation.Title = title;
      designation.EmployeeType = employeeType;
      designation.Status = StatusEnum.Active;
      Container.Persist(ref designation);
      return designation;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "Title")]
    public IList<NODesignation> ShowAllDesigntions()
    {
      return Container.Instances<NODesignation>().ToList();
    }
    #endregion

    #region Job Type
    public NOJobType NewJobType(string title, bool hasOverTime)
    {
      NOJobType jobType = Container.NewTransientInstance<NOJobType>();

      jobType.Title = title;
      jobType.HasOverTime = hasOverTime;
      jobType.Status = StatusEnum.Active;
      Container.Persist(ref jobType);
      return jobType;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "Title")]
    public IList<NOJobType> ShowAllJpbTypes()
    {
      return Container.Instances<NOJobType>().ToList();
    }
    #endregion

    #region Shift
    public NOWorkingShift NewWorkingShift(string shiftName, TimeSpan startTime, TimeSpan endTime)
    {
      NOWorkingShift shift = Container.NewTransientInstance<NOWorkingShift>();
      shift.ShiftName = shiftName;
      shift.StartTime = startTime;
      shift.EndTime = endTime;
      shift.Status = StatusEnum.Active;
      Container.Persist(ref shift);
      return shift;
    }
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "ShiftName", "StartTime", "EndTime", "Status")]
    public IList<NOWorkingShift> ShowAllWorkingShifts()
    {
      return Container.Instances<NOWorkingShift>().ToList();
    }
    #endregion

    #region Employee Grade
    public NOGrade NewGrade(string title, decimal minSalary, decimal maxSalary)
    {
      NOGrade grade = Container.NewTransientInstance<NOGrade>();

      grade.GradeTitle = title;
      grade.MinSalary = minSalary;
      grade.MaxSalary = maxSalary;
      grade.Status = StatusEnum.Active;
      Container.Persist(ref grade);
      return grade;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "GradeTitle", "MinSalary", "MaxSalary")]
    public IList<NOGrade> ShowAllGrades()
    {
      return Container.Instances<NOGrade>().ToList();
    }
    #endregion

    #region LeaveType
    public NOLeaveType NewLeaveType(string leaveName, int leavesPerYear,
    [Optionally] bool canEmployeesApplyBeyondTheCurrentLeaveBalance, [Optionally] bool willLeaveCarriedForward,
    [Optionally] int? percentageOfLeaveCarriedForward)
    {
      NOLeaveType leaveType = Container.NewTransientInstance<NOLeaveType>();

      leaveType.LeaveName = leaveName;
      leaveType.LeavesPerYear = leavesPerYear;
      leaveType.CanEmployeesApplyBeyondTheCurrentLeaveBalance = canEmployeesApplyBeyondTheCurrentLeaveBalance;
      leaveType.WillLeaveCarriedForward = willLeaveCarriedForward;
      if (percentageOfLeaveCarriedForward != null)
        leaveType.PercentageOfLeaveCarriedForward = (int) percentageOfLeaveCarriedForward;
      leaveType.Status = StatusEnum.Active;
      Container.Persist(ref leaveType);
      return leaveType;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "LeaveName", "LeavesPerYear", "CanEmployeesApplyBeyondTheCurrentLeaveBalance", "WillLeaveCarriedForward", "PercentageOfLeaveCarriedForward")]
    public IList<NOLeaveType> ShowAllLeaveTypes()
    {
      return Container.Instances<NOLeaveType>().ToList();
    }
    #endregion

    #region Public Holiday
    public NOPublicHoliday NewPublicHoliday(string holidayName, DateTime startDate, DateTime endDate)
    {
      NOPublicHoliday publicHoliday = Container.NewTransientInstance<NOPublicHoliday>();

      publicHoliday.HolidayName = holidayName;
      publicHoliday.StartDate = startDate;
      publicHoliday.EndDate = endDate;
      publicHoliday.Status = StatusEnum.Active;
      Container.Persist(ref publicHoliday);
      return publicHoliday;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "HolidayName", "StartDate", "EndDate")]
    public IList<NOPublicHoliday> ShowAllHolidays()
    {
      NOSession currentSession = Container.Instances<NOSession>().Where(w => w.IsSessionClosed).FirstOrDefault();

      if (currentSession != null)
        return Container.Instances<NOPublicHoliday>().Where(w => w.StartDate >= currentSession.StartDate &&
                                                            w.EndDate <= currentSession.EndDate).ToList();
      return Container.Instances<NOPublicHoliday>().ToList();
    }
    #endregion
  }
}
