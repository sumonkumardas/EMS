using MediatR;

namespace SinePulse.EMS.Messages.BankMessages
{
  public class AddBankRequestMessage : IRequest<AddBankResponseMessage>
  {
    public string BankName { get; set; }
    public long BranchMediumId { get; set; }
    public string CurrentUserName { get; set; }
  }
}