using NakedObjects;
using NakedObjects.Menu;
using NakedObjects.Value;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Attendance;
using SinePulse.EMS.NO.Domain.Banks;
using SinePulse.EMS.NO.Domain.Designations;
using SinePulse.EMS.NO.Domain.Features;
using SinePulse.EMS.NO.Domain.Leaves;
using SinePulse.EMS.NO.Domain.Repositories;
using SinePulse.EMS.NO.Domain.Shared;
using SinePulse.EMS.NO.Domain.UserManagement;
using SinePulse.EMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Employees
{
  [Table("Employees")]
  [DisplayName("Employee")]
  public class NOEmployee : NOBaseEntity
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

    public string Title()
    {
      var t = Container.NewTitleBuilder();

      string title = this.FullName + " (" + this.UserCode + ")";

      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    [Required, StringLength(250), MemberOrder(10)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string FullName { get; set; }
    [Required, MemberOrder(15), Disabled]
    [StringLength(25)]
    [Index("IX_Employee_EmployeeCode", IsClustered = false, IsUnique = true)]
    public virtual string UserCode { get; set; }
    [Optionally, MemberOrder(20)]
    public virtual DateTime? DOB { get; set; }
    [Optionally, StringLength(50), MemberOrder(30)]
    public virtual string NationalId { get; set; }
    [Optionally, StringLength(250), MemberOrder(40)]
    public virtual string MobileNo { get; set; }
    [Optionally, StringLength(150), MemberOrder(50)]
    public virtual string EmailAddress { get; set; }
    [MemberOrder(60), Optionally]
    public virtual DateTime? JoiningDate { get; set; }
    [MemberOrder(70), Optionally]
    public virtual EmployeeTypeEnum EmployeeType { get; set; }
    [MemberOrder(80), Required]
    public virtual StatusEnum Status { get; set; }
    [NotMapped, MemberOrder(15)]
    public virtual Image Photo
    {
      get
      {
        if (PhotoContent != null)
        {
          return new Image(PhotoContent, PhotoName, PhotoMime);
        }
        else
        {
          return null;
        }
      }
    }

    [NakedObjectsIgnore, Optionally]
    public virtual byte[] PhotoContent { get; set; }

    [NakedObjectsIgnore, Optionally]
    public virtual string PhotoName { get; set; }

    [NakedObjectsIgnore, Optionally]
    public virtual string PhotoMime { get; set; }

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
    [MemberOrder(100), Optionally]
    public virtual NOEmployee ReportTo { get; set; }
    [MemberOrder(110), Optionally]
    public virtual NODesignation Designation { get; set; }
    [MemberOrder(130), Optionally]
    public virtual NOJobType JobType { get; set; }
    [MemberOrder(140), Optionally]
    public virtual NOGrade Grade { get; set; }
    [MemberOrder(150), Optionally]
    public virtual NOAddress PresentAddress { get; set; }
    [MemberOrder(160), Optionally]
    public virtual NOAddress PermanentAddress { get; set; }
    [MemberOrder(170), Optionally]
    public virtual BranchMedium Branch { get; set; }
    [MemberOrder(100), Optionally, Disabled]
    [NakedObjectsIgnore]
    public virtual NOLoginUser LoginUser { get; set; }
    #endregion

    #region EducationalQualifications (collection)
    private ICollection<NOEducationalQualification> _qualifications = new List<NOEducationalQualification>();
    [MemberOrder(200)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "InstituteName", "DegreeName", "MajorSubject", "PassingYear")]
    public virtual ICollection<NOEducationalQualification> EducationalQualifications
    {
      get
      {
        return _qualifications;
      }
      set
      {
        _qualifications = value;
      }
    }
    #endregion

    #region Get Properties
    [NotMapped]
    [MemberOrder(210)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "InTime", "OutTime")]
    public IList<NOEmployeeAttendance> Attendance
    {
      get
      {
        return Container.Instances<NOEmployeeAttendance>().Where(w => w.Employee.Id == this.Id).OrderByDescending(o => o.AttendanceDate).ThenBy(t => t.InTime).ToList();
      }
    }
    #endregion 

    #region Behavior
    public void AddPersonalDetails(string fatherName, string motherName, [Optionally] string spouseName, GenderEnum gender, ReligionEnum religion, BloodGroupEnum bloodGroup)
    {
      NOEmployeePersonalInfo personalInfo = Container.NewTransientInstance<NOEmployeePersonalInfo>();
      personalInfo.FatherName = fatherName;
      personalInfo.MotherName = motherName;
      personalInfo.SpouseName = spouseName;
      personalInfo.Gender = gender;
      personalInfo.Religion = religion;
      personalInfo.BloodGroup = bloodGroup;
      personalInfo.Employee = this;
      Container.Persist(ref personalInfo);
    }
    public void AddEducationalQualification(string instituteName, EducationDegreeEnum degreeName, string majorSubject, string passingYear)
    {
      NOEducationalQualification qualification = Container.NewTransientInstance<NOEducationalQualification>();
      qualification.InstituteName = instituteName;
      qualification.DegreeName = degreeName;
      qualification.MajorSubject = majorSubject;
      qualification.PassingYear = passingYear;
      qualification.Employee = this;
      Container.Persist(ref qualification);
    }
    public void AddPresentAddress(string street1, [Optionally]string street2, string postalCode, string city)
    {
      NOAddress address = Container.NewTransientInstance<NOAddress>();
      address.Street1 = street1;
      address.Street2 = street2;
      address.PostalCode = postalCode;
      address.City = city;
      Container.Persist(ref address);
      this.PresentAddress = address;
    }
    public void AddPermanentAddress(bool sameAsPresentAddress, [Optionally]string street1, [Optionally]string street2, [Optionally]string postalCode, [Optionally]string city)
    {
      if (sameAsPresentAddress)
      {
        PermanentAddress = PresentAddress;
      }
      else
      {
        NOAddress address = Container.NewTransientInstance<NOAddress>();
        address.Street1 = street1;
        address.Street2 = street2;
        address.PostalCode = postalCode;
        address.City = city;
        Container.Persist(ref address);
        this.PermanentAddress = address;
      }
    }
    public void AddOrChangePhoto(Image newImage)
    {
      PhotoContent = newImage.GetResourceAsByteArray();
      PhotoName = newImage.Name;
      PhotoMime = newImage.MimeType;
    }
    //public IList<NOBranch> Choices0AddOrChangeBranch()
    //{
    //  return Container.Instances<NOBranch>().ToList();
    //}
    //public IList<BranchMedium> Choices1AddOrChangeBranch(NOBranch branch)
    //{
    //  if (branch != null)
    //  {
    //    return Container.Instances<BranchMedium>().Where(w => w.Branch.Id == branch.Id).ToList();
    //  }
    //  else
    //  {
    //    return new List<BranchMedium>();
    //  }
    //}
    public void ApplyLeave (NOLeaveType leaveType, DateTime startDate, DateTime endDate, string reason)
    {
      NOEmployeeLeave employeeLeave = Container.NewTransientInstance<NOEmployeeLeave>();
      employeeLeave.LeaveType = leaveType;
      employeeLeave.StartDate = startDate;
      employeeLeave.EndDate = endDate;
      employeeLeave.Reason = reason;
      employeeLeave.LeaveStatus = LeaveStatusEnum.Pending;
      employeeLeave.Employee = this;
      Container.Persist(ref employeeLeave);
    }
    public void ApproveLeave(NOEmployee employee, NOEmployeeLeave leave, bool isApproved)
    {
      if (isApproved)
      {
        leave.LeaveStatus = LeaveStatusEnum.Approved;
      }
      else
      {
        leave.LeaveStatus = LeaveStatusEnum.Cancelled;
      }
    }
    public IList<NOEmployee> Choices0ApproveLeave()
    {
      return Container.Instances<NOEmployee>().Where(w => w.ReportTo.Id == this.Id).ToList();
    }
    public IList<NOEmployeeLeave> Choices1ApproveLeave(NOEmployee employee)
    {
      if (employee != null)
        return Container.Instances<NOEmployeeLeave>().Where(w => w.Employee.Id == employee.Id && w.LeaveStatus == LeaveStatusEnum.Pending).ToList();

      return new List<NOEmployeeLeave>();
    }

    #region AddLoginUser

    [DisplayName("Add Login User")]
    public void AddLoginUser([DataType(DataType.Password)] string password,
      [DataType(DataType.Password)] string confirmPassword, NORole role)
    {
      NOLoginUser user = Container.NewTransientInstance<NOLoginUser>();

      user.Id = Guid.NewGuid().ToString();
      user.UserName = this.UserCode;
      user.Email = this.UserCode;
      user.EmailConfirmed = false;
      user.PasswordHash = PasswordHash.HashPassword(password);
      user.SecurityStamp = Guid.NewGuid().ToString();
      user.PhoneNumberConfirmed = false;
      user.TwoFactorEnabled = false;
      user.LockoutEnabled = false;
      user.AccessFailedCount = 0;

      Container.Persist(ref user);

      NOUserRoles userRole = Container.NewTransientInstance<NOUserRoles>();
      userRole.LoginUser = user;
      userRole.Role = role;
      Container.Persist(ref userRole);

      this.LoginUser = user;
    }

    public IList<NORole> Choices2AddLoginUser()
    {
      IList<NORole> roles = Container.Instances<NORoleFeatures>().Select(s => s.Role).ToList();

      return roles.Distinct().OrderBy(o => o.Name).ToList();
    }

    public string ValidateAddLoginUser(string password, string confirmPassword, NORole role)
    {
      if (password != confirmPassword)
      {
        return "Password does not match";
      }

      return null;
    }

    //public bool HideAddLoginUser()
    //{
    //  if (String.IsNullOrEmpty(this.EmailAddress)) return true;

    //  IList<NOFeature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

    //  NOFeature feature =
    //    features.Where(w => w.FeatureCode == (int)NOFeature.UserManagementEnum.AddLoginUser
    //                        && w.FeatureType.FeatureTypeName == NOFeatureType.FeatureTypeEnum.UserManagement.ToString())
    //      .FirstOrDefault();

    //  if (feature == null) return true;

    //  NOLoginUser user =
    //    Container.Instances<NOLoginUser>().Where(w => w.Email == this.UserCode).FirstOrDefault();

    //  if (user != null) return true;

    //  return false;
    //}

    #endregion

    #region Change Password

    public void ChangePassword([DataType(DataType.Password)] string password,
      [DataType(DataType.Password)] string confirmPassword)
    {
      if (this.LoginUser != null)
      {
        this.LoginUser.PasswordHash = PasswordHash.HashPassword(password);
        Container.InformUser("Password has been changed.");
      }
    }

    //public bool HideChangePassword()
    //{
    //  IList<NOFeature> features = LoggedInUserRepository.GetFeatureListByLoginUser();

    //  NOFeature feature =
    //    features.Where(w => w.FeatureCode == (int)NOFeature.UserManagementEnum.ChangePassword
    //                        && w.FeatureType.FeatureTypeName == NOFeatureType.FeatureTypeEnum.UserManagement.ToString())
    //      .FirstOrDefault();

    //  if (feature == null)
    //    return true;
    //  if (this.LoginUser == null) return true;
    //  return false;
    //}

    public string ValidateChangePassword(string password, string confirmPassword)
    {
      if (password != confirmPassword)
      {
        return "Password does not match";
      }

      return null;
    }

    #endregion

    public void AddAttendance(DateTime attendanceDate, TimeSpan inTime, TimeSpan outTime)
    {
      NOEmployeeAttendance attendanceData = Container.NewTransientInstance<NOEmployeeAttendance>();
      attendanceData.AttendanceDate = attendanceDate;
      attendanceData.InTime = inTime;
      attendanceData.OutTime = outTime;
      attendanceData.Employee = this;
      Container.Persist(ref attendanceData);
    }
    #endregion

    public static void Menu(IMenu menu)
    {
      menu.AddAction("AddPersonalDetails");
      menu.AddAction("AddEducationalQualification");
      menu.AddAction("AddPresentAddress");
      menu.AddAction("AddPermanentAddress");
      menu.AddAction("AddOrChangePhoto");

      IMenu leaveMeu = menu.CreateSubMenu("Leave");
      leaveMeu.AddAction("ApplyLeave");
      leaveMeu.AddAction("ApproveLeave");

      IMenu loginMenu = menu.CreateSubMenu("Login");
      loginMenu.AddAction("AddLoginUser");
      loginMenu.AddAction("ChangePassword");

      menu.AddAction("AddAttendance");
    }
  }
}
