using MediatR;

namespace SinePulse.EMS.Messages.OnlinePaymentMessages
{
  public class GetPaymentInfoRequestMessage : IRequest<GetPaymentInfoResponseMessage>
  {
    public string TransactionId { get; set; }
  }
}