using FluentValidation.Results;

namespace SinePulse.EMS.Messages.StudentContactPersonMessages
{
  public class AddStudentContactPersonImageResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public string PreviousImage { get; }
    public AddStudentContactPersonImageResponseMessage(ValidationResult validationResult, string previousImage)
    {
      ValidationResult = validationResult;
      PreviousImage = previousImage;
    }

    
  }
}