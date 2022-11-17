using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.BranchMessages
{
  public class ShowBranchListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<BranchMessageModel> Branches { get; }

    public ShowBranchListResponseMessage(ValidationResult validationResult, IEnumerable<BranchMessageModel> branches)
    {
      ValidationResult = validationResult;
      Branches = branches;
    }

    public ShowBranchListResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}