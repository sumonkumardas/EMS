using System.Collections.Generic;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public interface IGetBranchMediumAccountHeadsUseCase
  {
    IEnumerable<BranchMediumAccountsHeadMessageModel>
      GetAccountHeads(GetBranchMediumAccountHeadsRequestMessage message);
  }
}