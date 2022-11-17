using FluentValidation.Results;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class AddEmployeeImageResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public string PreviousImage { get;}
    public AddEmployeeImageResponseMessage(ValidationResult validationResult, string previousImage)
    {
      ValidationResult = validationResult;
      PreviousImage = previousImage;
    }

    
  }
}