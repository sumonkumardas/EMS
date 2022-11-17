using FluentValidation.Results;

namespace SinePulse.EMS.Messages.CommitteeMemberMessages
{
  public class AddOrChangeCommitteeMemberImageResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public string PreviousImage { get; set; }

    public AddOrChangeCommitteeMemberImageResponseMessage(ValidationResult validationResult, string previousImage)
    {
      ValidationResult = validationResult;
      PreviousImage = previousImage;
    }
  }
}