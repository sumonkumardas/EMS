using System.Collections.Generic;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.UseCases.Branches
{
  public interface IShowBranchListUseCase
  {
    IEnumerable<BranchMessageModel> ShowBranchList(ShowBranchListRequestMessage message);
  }
}