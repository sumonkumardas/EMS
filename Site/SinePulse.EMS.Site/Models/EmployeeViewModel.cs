
using SinePulse.EMS.Messages.Model.Enums;
using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Attendance;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Site.Models
{
  public class EmployeeViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string FullName { get; set; }
    public string EmployeeCode { get; set; }
    public DateTime? DOB { get; set; }
    public string NationalId { get; set; }
    public string MobileNo { get; set; }
    public string EmailAddress { get; set; }
    public DateTime? JoiningDate { get; set; }
    public string ImageUrl { get; set; }
    public string BankAccountNo { get; set; }
    public EmployeeTypeEnum EmployeeType { get; set; }
    public DesignationMessageModel Designation { get; set; }
    public InstituteViewModel Institute { get; set; }
    public BranchViewModel Branch { get; set; }
    public BranchMediumViewModel BranchMedium { get; set; }
    public GradeViewModel Grade { get; set; }
    public EmployeeViewModel ReportTo { get; set; }
    public bool isAccountRegistered { get; set; }
    public long? ObjectId { get; set; }
    public long? ReportToId { get; set; }
    public JobTypeViewModel JobType { get; set; }
    public StatusEnum Status { get; set; }
    public AssociationType AssociatType { get; set; }
    public DateTime? AttendanceStartDate { get; set; }
    public DateTime? AttendanceEndDate { get; set; }
    public DateTime? LeaveFilterStartDate { get; set; }
    public DateTime? LeaveFilterEndDate { get; set; }
  }
}