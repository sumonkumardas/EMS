using MediatR;

namespace SinePulse.EMS.Messages.BankMessages
{
  public class EditBankRequestMessage : IRequest<EditBankResponseMessage>
  {
    public long BankId { get; set; }
    public string BankName { get; set; }
    public string CurrentUserName { get; set; }
  }
}