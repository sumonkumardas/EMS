using MediatR;

namespace SinePulse.EMS.Messages.EmployeeSalaryMessages
{
  public class GetEmployeeSalaryListRequestMessage : IRequest<GetEmployeeSalaryListResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}