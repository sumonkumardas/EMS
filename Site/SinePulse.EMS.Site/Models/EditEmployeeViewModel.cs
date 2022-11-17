using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Persistence.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeMessages;

namespace SinePulse.EMS.Site.Models
{
  public class EditEmployeeViewModel: BaseViewModel
  {
    public long Id { get; set; }
    public string FullName { get; set; }
    public DateTime? DOB { get; set; }
    public string NationalId { get; set; }
    public string MobileNo { get; set; }
    public string EmailAddress { get; set; }
    public DateTime? JoiningDate { get; set; }
    public string BankAccountNo { get; set; }
    public EmployeeTypeEnum EmployeeType { get; set; }
    public AssociationType AssociatedType { get; set; }
    public long? DesignationId { get; set; }
    public long? ObjectId { get; set; }
    public long? GradeId { get; set; }
    public long? ReportToId { get; set; }
    public long? JobTypeId { get; set; }
    public StatusEnum Status { get; set; }
    public List<BranchMediumViewModel> Branches { get; set; }
    public List<ShowEmployeeListResponseMessage.Employee> Employees { get; set; }
    public List<DesignationViewModel> Designations { get; set; }
    public List<GradeViewModel> Grades { get; set; }
    public List<JobTypeViewModel> JobTypes { get; set; }

    }
}