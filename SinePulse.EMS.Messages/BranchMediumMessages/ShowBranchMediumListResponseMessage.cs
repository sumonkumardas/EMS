using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class ShowBranchMediumListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<BranchMediumMessageModel> BranchMediumList { get; }

    public ShowBranchMediumListResponseMessage(ValidationResult validationResult, List<BranchMediumMessageModel> branchMediumList)
    {
      ValidationResult = validationResult;
      BranchMediumList = branchMediumList;
    }
  }
}