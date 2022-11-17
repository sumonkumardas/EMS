using System;
using MediatR;

namespace SinePulse.EMS.Messages.OnlinePaymentMessages
{
  public class UpdateTransactionInfoRequestMessage : IRequest<UpdateTransactionInfoResponseMessage>
  {
    public virtual string TransactionId { get; set; }
    public virtual decimal StoreAmount { get; set; }
    public virtual string ValidationId { get; set; }
    public virtual string CardType { get; set; }
    public virtual DateTime PaymentDate{ get; set; }
    public string CurrentUserName { get; set; }
  }
}