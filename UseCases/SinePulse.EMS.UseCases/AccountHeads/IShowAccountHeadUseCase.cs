using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public interface IShowAccountHeadUseCase
  {
    AccountHeadMessageModel ShowAccountHead(ShowAccountHeadRequestMessage message);
  }
}