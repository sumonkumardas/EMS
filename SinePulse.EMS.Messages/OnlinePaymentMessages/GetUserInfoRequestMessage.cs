using MediatR;

namespace SinePulse.EMS.Messages.OnlinePaymentMessages
{
  public class GetUserInfoRequestMessage : IRequest<GetUserInfoResponseMessage>
  {
    public string EmployeeCode { get; set; }
  }
}