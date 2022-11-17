using System.Collections.Generic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public interface IAddBranchMediumAccountHeadUseCase
  {
    BranchMediumAccountHead
      AddAccountHead(AddBranchMediumAccountHeadRequestMessage message);
  }
}