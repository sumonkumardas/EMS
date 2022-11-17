using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.TransactionMessages
{
  public class ShowTransactionRequestMessage : IRequest<ShowTransactionResponseMessage>
  {
    public long TransactionId { get; set; }
  }
}