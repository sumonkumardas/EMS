using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages
{
  public class GetBranchMediumAccountHeadsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<BranchMediumAccountsHeadMessageModel> AccountHeads { get; set; }

    public GetBranchMediumAccountHeadsResponseMessage(ValidationResult validationResult,
      IEnumerable<BranchMediumAccountsHeadMessageModel> accountHeads)
    {
      ValidationResult = validationResult;
      AccountHeads = accountHeads;
    }
  }
}