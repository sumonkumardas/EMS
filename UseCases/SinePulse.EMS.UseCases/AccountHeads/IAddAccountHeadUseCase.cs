using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public interface IAddAccountHeadUseCase
  {
    AccountHeadMessageModel AddAccountHead(AddAccountHeadRequestMessage message);
  }
}