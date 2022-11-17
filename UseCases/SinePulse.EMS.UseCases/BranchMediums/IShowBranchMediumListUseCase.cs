
using SinePulse.EMS.Messages.BranchMediumMessages;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public interface IShowBranchMediumListUseCase
  {
    List<BranchMediumMessageModel> ShowBranchMediumList(ShowBranchMediumListRequestMessage request);
  }
}