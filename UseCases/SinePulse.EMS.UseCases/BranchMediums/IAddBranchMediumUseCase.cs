using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.BranchMediumMessages;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public interface IAddBranchMediumUseCase
  {
    BranchMedium AddBranchMedium(AddBranchMediumRequestMessage request);
  }
}