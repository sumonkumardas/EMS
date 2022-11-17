using FluentValidation;
using SinePulse.EMS.Messages.AcademicFeeMessages;

namespace SinePulse.EMS.UseCases.AcademicFees
{
  public class GetAcademicFeesRequestMessageValidator : AbstractValidator<GetAcademicFeesRequestMessage>
  {
    public GetAcademicFeesRequestMessageValidator()
    {
    }
  }
}