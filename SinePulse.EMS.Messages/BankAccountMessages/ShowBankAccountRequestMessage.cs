using MediatR;

namespace SinePulse.EMS.Messages.BankAccountMessages
{
  public class ShowBankAccountRequestMessage : IRequest<ShowBankAccountResponseMessage>
  {
    public long BankAccountId { get; set; }
  }
}