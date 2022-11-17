using FluentValidation.Results;

namespace SinePulse.EMS.Messages.AcademicFeeMessages
{
  public class AddAcademicFeeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long AcademicFeeId { get; }

    public AddAcademicFeeResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}