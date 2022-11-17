using NakedObjects;
using NakedObjects.Value;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Attendance;
using SinePulse.EMS.NO.Domain.Repositories;
using SinePulse.EMS.NO.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Students
{
  [Table("Students")]
  [DisplayName("Student")]
  public class NOStudent : NOBaseEntity
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

      string title = this.FullName + " (" + this.StudentId + ")";

      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    [Required, StringLength(250), MemberOrder(10)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string FullName { get; set; }
    [Required, MemberOrder(15), Disabled]
    [StringLength(25)]
    [Index("IX_Student_StudentId", IsClustered = false, IsUnique = true)]
    public virtual string StudentId { get; set; }
    [Required, MemberOrder(20)]
    [Mask("d")]
    public virtual DateTime DOB { get; set; }
    [MemberOrder(30), Required]
    public virtual RelationTypeEnum Guardian { get; set; }
    [MemberOrder(80), Required]
    public virtual StatusEnum Status { get; set; }
    
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

    #region Get Properties
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
    [NotMapped, MemberOrder(25)]
    public virtual string Class
    {
      get
      {
        BranchMedium medium = Container.Instances<NOStudentSection>().Select(s => s.Section.BranchMedium).FirstOrDefault();
        if (medium != null)
        {
          NOSession session = medium.Sessions.Where(w => w.IsSessionClosed).FirstOrDefault();
          if(session!= null)
          {
            NOStudentSection section = Container.Instances<NOStudentSection>().Where(w => w.Section.Session.Id == session.Id).FirstOrDefault();
            if (section != null)
            {
              return section.Section.Title() + " -> " + section.RollNo.ToString();
            }
          }
        }
        
        return null;
      }
    }
    #endregion

    #region  Navigation Properties
    [Optionally, MemberOrder(100)]
    public virtual NOContactInfo ContactInfo { get; set; }
    #endregion

    #region Contact Person (collection)
    private ICollection<NOContactPerson> _contactPersons = new List<NOContactPerson>();
    [MemberOrder(200)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "RelationType", "PhoneNo", "EmailAddress", "NID")]
    public virtual ICollection<NOContactPerson> ContactPersons
    {
      get
      {
        return _contactPersons;
      }
      set
      {
        _contactPersons = value;
      }
    }
    #endregion

    #region Get Properties
    [NotMapped]
    [MemberOrder(200)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "AttendanceDate", "InTime", "OutTime")]
    public IList<NOStudentAttendance> Attendance
    {
      get
      {
        return Container.Instances<NOStudentAttendance>().Where(w => w.Student.Id == this.Id).OrderByDescending(o => o.AttendanceDate).ThenBy(t => t.InTime).ToList();
      }
    }
    #endregion 

    #region Behavior
    public void AddOrChangePhoto(Image newImage)
    {
      PhotoContent = newImage.GetResourceAsByteArray();
      PhotoName = newImage.Name;
      PhotoMime = newImage.MimeType;
    }
    public void AddSection(NOInstitute institute, NOBranch branch, BranchMedium medium, NOSession session, 
      NOSection section, int rollNo)
    {
      NOStudentSection studentSection = Container.NewTransientInstance<NOStudentSection>();
      studentSection.RollNo = rollNo;
      studentSection.Status = StatusEnum.Active;
      studentSection.Student = this;
      studentSection.Section = section;
      Container.Persist(ref studentSection);
    }
    public bool HideAddSection()
    {
      NOStudentSection section = GetSection();
      if (section != null) return true;
      return false;
    }
    public IList<NOInstitute> Choices0AddSection()
    {
      return Container.Instances<NOInstitute>().ToList();
    }
    public IList<NOBranch> Choices1AddSection(NOInstitute institute)
    {
      if (institute != null) return institute.Branches;

      return new List<NOBranch>();
    }
    public IList<BranchMedium> Choices2AddSection(NOBranch branch)
    {
      if (branch != null) return branch.Mediums;

      return new List<BranchMedium>();
    }
    public IList<NOSession> Choices3AddSection(BranchMedium medium)
    {
      if (medium != null) return medium.Sessions;

      return new List<NOSession>();
    }
    public IList<NOSection> Choices4AddSection(BranchMedium medium, NOSession session)
    {
      if (medium != null && session != null) return medium.Classes.Where(w => w.Session.Id == session.Id).ToList();

      return new List<NOSection>();
    }
    public void AddContactInfo(string phoneNo, [Optionally]string email)
    {
      NOContactInfo contactInfo = Container.NewTransientInstance<NOContactInfo>();
      contactInfo.PhoneNo = phoneNo;
      contactInfo.EmailAddress = email;
      contactInfo.Status = StatusEnum.Active;
      Container.Persist(ref contactInfo);
      this.ContactInfo = contactInfo;
    }
    public void AddContactPerson(RelationTypeEnum relationType, string name, string phoneNo, [Optionally]string email, 
      string nationalId, [Optionally]string profession, [Optionally]string designation, [Optionally]string officeNameNAddress, 
      [Optionally]string officeContactNo)
    {
      NOContactPerson person = Container.NewTransientInstance<NOContactPerson>();
      person.RelationType = relationType;
      person.Name = name;
      person.PhoneNo = phoneNo;
      person.EmailAddress = email;
      person.NID = nationalId;
      person.Profession = profession;
      person.Designation = designation;
      person.OfficeNameAddress = officeNameNAddress;
      person.OfficeContactNo = officeContactNo;
      person.Status = StatusEnum.Active;
      person.Student = this;
      Container.Persist(ref person);
    }

    public void AddAttendance(DateTime attendanceDate, TimeSpan inTime, TimeSpan outTime)
    {
      NOStudentAttendance attendanceData = Container.NewTransientInstance<NOStudentAttendance>();
      attendanceData.AttendanceDate = attendanceDate;
      attendanceData.InTime = inTime;
      attendanceData.OutTime = outTime;
      attendanceData.Student = this;
      Container.Persist(ref attendanceData);
    }
    #endregion

    private NOStudentSection GetSection()
    {
      return Container.Instances<NOStudentSection>().Where(w => w.Student.Id == this.Id).FirstOrDefault();
    }
  }
}
