using FluentValidation.Results;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class AddOrChangeStudentImageResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public string PreviousImage { get; set; }

    public AddOrChangeStudentImageResponseMessage(ValidationResult validationResult, string previousImage)
    {
      ValidationResult = validationResult;
      PreviousImage = previousImage;
    }
  }
}