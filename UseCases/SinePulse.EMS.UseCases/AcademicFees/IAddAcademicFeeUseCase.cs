using SinePulse.EMS.Messages.AcademicFeeMessages;

namespace SinePulse.EMS.UseCases.AcademicFees
{
  public interface IAddAcademicFeeUseCase
  {
    void AddAcademicFee(AddAcademicFeeRequestMessage message);
  }
}