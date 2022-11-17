using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BranchMessages
{
  public class EditBranchResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditBranchResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}