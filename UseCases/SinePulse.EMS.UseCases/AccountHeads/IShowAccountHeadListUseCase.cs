using System.Collections.Generic;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public interface IShowAccountHeadListUseCase
  {
    IEnumerable<AccountHeadMessageModel> ShowAccountHeadList(ShowAccountHeadListRequestMessage message);
  }
}