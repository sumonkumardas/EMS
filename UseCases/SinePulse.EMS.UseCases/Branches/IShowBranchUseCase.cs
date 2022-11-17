using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.BranchMessages;

namespace SinePulse.EMS.UseCases.Branches
{
  public interface IShowBranchUseCase
  {
    Branch ShowBranch(ShowBranchRequestMessage request);
  }
}