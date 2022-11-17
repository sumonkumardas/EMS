using MediatR;

namespace SinePulse.EMS.Messages.EmployeePersonalInfoMessages
{
  public class GetEmployeePersonalInfoRequestMessage : IRequest<GetEmployeePersonalInfoResponseMessage>
  {
    public long EmployeeId { get; set; }
  }
}