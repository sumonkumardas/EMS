using MediatR;

namespace SinePulse.EMS.Messages.EmployeeAddressMessages
{
  public class GetEmployeeAddressRequestMessage : IRequest<GetEmployeeAddressResponseMessage>
  {
    public long EmployeeId { get; set; }
  }
}