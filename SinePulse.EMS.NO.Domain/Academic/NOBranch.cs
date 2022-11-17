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
using NakedObjects.Menu;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.NO.Domain.Employees;
using SinePulse.EMS.NO.Domain.Designations;

namespace SinePulse.EMS.NO.Domain.Academic
{
  [Table("Branches")]
  [DisplayName("Branch")]
  public class NOBranch : NOBaseEntity
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

      string title = this.Institute.OrganisationName + " -> " + this.BranchName;

      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    [Required, StringLength(250), MemberOrder(10)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string BranchName { get; set; }
    [Required, StringLength(50), MemberOrder(20)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string BranchCode { get; set; }
    [MemberOrder(30), Optionally]
    [StringLength(15)]
    [Description("Example: +8801523456789")]
    [RegEx(Validation = @"^(?:\+8801)?\d{9}\r?$", Message = "Not a valid mobile no")]
    public virtual string MobileNo { get; set; }
    [MemberOrder(40), Optionally]
    [StringLength(250)]
    [RegEx(Validation = @"^[\-\w\.]+@[\-\w\.]+\.[A-Za-z]+$", Message = "Not a valid email address")]
    public virtual string Email { get; set; }
    [MemberOrder(550), Optionally]
    [StringLength(15)]
    [Description("Example: +8809145368")]
    [RegEx(Validation = @"^(?:\+8801)?\d{9}\r?$", Message = "Not a valid fax no")]
    public virtual string Fax { get; set; }
    [MemberOrder(70), Optionally]
    [StringLength(500), NakedObjectsIgnore]
    public virtual string MapIFrame { get; set; }
    [MemberOrder(580), Required]
    public virtual StatusEnum Status { get; set; }
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
    [Required, MemberOrder(1)]
    [DisplayName("Institute")]
    public virtual NOInstitute Institute { get; set; }
    [Optionally, MemberOrder(510)]
    [DisplayName("Address")]
    public virtual NOAddress NOAddress { get; set; }
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

    #region Shifts (collection)
    private ICollection<NOShift> _shifts = new List<NOShift>();
    [MemberOrder(150)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "StartTime", "EndTime")]
    public virtual ICollection<NOShift> Shifts
    {
      get
      {
        return _shifts;
      }
      set
      {
        _shifts = value;
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

    #region Get Properties
    [NotMapped, MemberOrder(510)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "WeeklyHolidays")]
    public virtual IList<BranchMedium> Mediums
    {
      get { return Container.Instances<BranchMedium>().Where(w => w.Branch.Id == this.Id).ToList(); }
    }

    [NotMapped, MemberOrder(150)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "StartDate", "EndDate", "IsSessionClosed")]
    public virtual IList<NOSession> Sessions
    {
      get
      {
        if (this.MySessions.Any()) return this.MySessions.ToList();

        if(this.Institute != null)
          if (this.Institute.Sessions.Any()) return this.Institute.Sessions.ToList();

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
      session.SessionType = ObjectTypeEnum.Branch;
      session.Status = StatusEnum.Active;
      Container.Persist(ref session);
      this.MySessions.Add(session);
    }
    public void AddShift(string shiftName, TimeSpan startTime, TimeSpan endTime)
    {
      NOShift shift = Container.NewTransientInstance<NOShift>();
      shift.ShiftName = shiftName;
      shift.StartTime = startTime;
      shift.EndTime = endTime;
      shift.ShiftType = ObjectTypeEnum.Branch;
      shift.Status = StatusEnum.Active;
      Container.Persist(ref shift);
      this.Shifts.Add(shift);
    }
    public void AddRoom(string roomName, int seatCapacity, int examSeatCapacity)
    {
      NORoom room = Container.NewTransientInstance<NORoom>();
      room.RoomName = roomName;
      room.SeatCapacity = seatCapacity;
      room.ExamSeatCapacity = examSeatCapacity;
      room.Status = StatusEnum.Active;
      room.Branch = this;
      Container.Persist(ref room);
    }
    public void AddMedium (NOShift shift, NOMedium medium, IEnumerable<WeekDays> weeklyHolidays)
    {
      WeekDays holiday = WeekDays.None;
      foreach (WeekDays day in weeklyHolidays)
      {
        if (holiday == WeekDays.None)
        {
          holiday = day;
        }
        else
        {
          holiday |= day;
        }
      }

      BranchMedium branchMedium = Container.NewTransientInstance<BranchMedium>();
      branchMedium.Branch = this;
      branchMedium.Shift = shift;
      branchMedium.Medium = medium;
      branchMedium.WeeklyHolidays = holiday;
      Container.Persist(ref branchMedium);
    }
    public IList<NOShift> Choices0AddMedium()
    {
      if (this.Shifts.Any()) return this.Shifts.ToList();
      return Container.Instances<NOShift>().Where(w => w.ShiftType == ObjectTypeEnum.Global).ToList();
    }
    #endregion

    public static void Menu(IMenu menu)
    {
      menu.AddAction("AddSession");
      menu.AddAction("AddShift");
      menu.AddAction("AddRoom");
      menu.AddAction("AddMedium");
    }
  }
}
