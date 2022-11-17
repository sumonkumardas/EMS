using MediatR;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Messages.EmployeeGradeMessages
{
  public class AddEmployeeGradeRequestMessage :IRequest<AddEmployeeGradeResponseMessage>
  {
    public string GradeTitle { get; set; }
    public decimal MinSalary { get; set; }
    public decimal MaxSalary { get; set; }
    public StatusEnum Status{ get; set; }
    public string CurrentUserName { get; set; }
  }
}