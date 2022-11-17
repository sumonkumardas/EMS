namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IPresentAddressInContactInfoChecker
  {
    bool IsPresentAddressAdded(long contactInfoId);
  }
}