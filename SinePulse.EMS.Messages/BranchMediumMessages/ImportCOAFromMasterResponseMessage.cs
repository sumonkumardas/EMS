using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class ImportCOAFromMasterResponseMessage
  {
    public ValidationResult ValidationResult { get; }

    public ImportCOAFromMasterResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}