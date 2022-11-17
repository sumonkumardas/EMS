using SinePulse.EMS.Messages.BranchMediumMessages;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public interface IEditBranchMediumUseCase
  {
    void EditBranchMedium(EditBranchMediumRequestMessage request);
  }
}