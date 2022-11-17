using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using System.Collections.Generic;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public interface IShowBranchMediumAccountsHeadListUseCase
  {
    List<BranchMediumAccountsHeadMessageModel> ShowBranchMediumAccountsHeadList(ShowBranchMediumAccountsHeadListRequestMessage message);
  }
}