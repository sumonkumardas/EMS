using MediatR;

namespace SinePulse.EMS.Messages.BankMessages
{
  public class ShowBankRequestMessage : IRequest<ShowBankResponseMessage>
  {
    public long BankId { get; set; }
  }
}