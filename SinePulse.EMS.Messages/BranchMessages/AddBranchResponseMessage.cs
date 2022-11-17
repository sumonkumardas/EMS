using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BranchMessages
{
  public class AddBranchResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long BranchId { get; }
    public string BranchName { get; }

    public AddBranchResponseMessage(ValidationResult validationResult, long branchId)
    {
      ValidationResult = validationResult;
      BranchId = branchId;
    }

    public AddBranchResponseMessage(long branchId, string branchName)
    {
      BranchId = branchId;
      BranchName = branchName;
    }
  }
}