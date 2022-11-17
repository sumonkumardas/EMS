using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.BranchMediumMessages;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public interface IDisplayBranchMediumUseCase
  {
    BranchMedium ShowBranchMedium(DisplayBranchMediumRequestMessage request);
  }
}