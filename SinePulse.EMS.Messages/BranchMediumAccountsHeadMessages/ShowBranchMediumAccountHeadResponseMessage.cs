using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages
{
  public class ShowBranchMediumAccountHeadResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public BranchMediumAccountsHeadMessageModel AccountHead { get; set; }

    public ShowBranchMediumAccountHeadResponseMessage(ValidationResult validationResult,
      BranchMediumAccountsHeadMessageModel accountHead)
    {
      ValidationResult = validationResult;
      AccountHead = accountHead;
    }
  }
}