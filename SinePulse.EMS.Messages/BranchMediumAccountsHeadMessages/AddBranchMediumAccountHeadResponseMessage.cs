using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages
{
  public class AddBranchMediumAccountHeadResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long AccountHeadId { get; set; }

    public AddBranchMediumAccountHeadResponseMessage(ValidationResult validationResult,
      long accountHeadId)
    {
      ValidationResult = validationResult;
      AccountHeadId = accountHeadId;
    }
  }
}