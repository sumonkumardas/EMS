using SinePulse.EMS.Messages.FeeCollection;

namespace SinePulse.EMS.UseCases.FeeCollections
{
  public interface IFeeCollectionUseCase
  {
    void CollectFee(FeeCollectionRequestMessage message);
  }
}