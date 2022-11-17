using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Attendance;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Employees
{
  public class EmployeeMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string FullName { get; set; }
    public string EmployeeCode { get; set; }
    public DateTime? DOB { get; set; }
    public string NationalId { get; set; }
    public string MobileNo { get; set; }
    public string EmailAddress { get; set; }
    public DateTime? JoiningDate { get; set; }
    public string BankAccountNo { get; set; }
    public EmployeeTypeEnum EmployeeType { get; set; }
    public StatusEnum Status { get; set; }
    public AssociationType AssociatedWith { get; set; }
    public bool isAccountRegistered { get; set; }
    public long? ReportToId { get; set; }
    public string ImageUrl { get; set; }
    public long? ObjectId { get; set; }
    #endregion

    #region Navigation Properties
    public InstituteMessageModel Institute { get; set; }
    public BranchMessageModel Branch { get; set; }
    public BranchMediumMessageModel BranchMedium { get; set; }
    public DesignationMessageModel Designation { get; set; }
    public GradeMessageModel Grade { get; set; }
    public JobTypeMessageModel JobType { get; set; }
    public AddressMessageModel PresentAddress { get; set; }
    public AddressMessageModel PermanentAddress { get; set; }
    public EmployeeMessageModel ReportTo { get; set; }

    #endregion

    #region Complex Properties

    public AssociationMessageModel Association { get; set; } = new AssociationMessageModel();

    #endregion

    #region Get Properties

    public ICollection<EmployeeAttendanceMessageModel> Attendances { get; set; }

    #endregion
  }
}