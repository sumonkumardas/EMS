using SinePulse.EMS.Messages.BranchMessages;

namespace SinePulse.EMS.UseCases.Branches
{
  public interface IEditBranchUseCase
  {
    void EditBranch(EditBranchRequestMessage request);
  }
}