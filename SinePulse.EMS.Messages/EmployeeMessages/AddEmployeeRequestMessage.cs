using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.ProjectPrimitives;
using System;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class AddEmployeeRequestMessage : MediatR.IRequest<AddEmployeeResponseMessage>
  {
    public string FullName { get; set; }
    public DateTime DOB { get; set; }
    public string NationalId { get; set; }
    public string MobileNo { get; set; }
    public string EmailAddress { get; set; }
    public DateTime JoiningDate { get; set; }
    public string BankAccountNo { get; set; }
    public EmployeeTypeEnum EmployeeType { get; set; }
    public AssociationType AssociationType { get; set; }
    public StatusEnum Status { get; set; }
    //public JobTypeEnum JobType { get; set; }
    public long DesignationId { get; set; }
    public long? ObjectId { get; set; }
    public long GradeId { get; set; }
    public long? ReportToId { get; set; }
    public long JobTypeId { get; set; }
    public string CurrentUserName { get; set; }
  }
}