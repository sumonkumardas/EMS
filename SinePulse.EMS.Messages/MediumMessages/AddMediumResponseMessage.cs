using FluentValidation.Results;

namespace SinePulse.EMS.Messages.MediumMessages
{
  public class AddMediumResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long MediumId { get; }

    public AddMediumResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
    
    public AddMediumResponseMessage(ValidationResult validationResult, long mediumId)
    {
      ValidationResult = validationResult;
      MediumId = mediumId;
    }
  }
}