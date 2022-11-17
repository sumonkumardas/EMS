using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class EditBranchMediumResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public EditBranchMediumResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}