using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SectionMessages
{
  public class AssignOrChangeRoomInSectionResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public AssignOrChangeRoomInSectionResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}