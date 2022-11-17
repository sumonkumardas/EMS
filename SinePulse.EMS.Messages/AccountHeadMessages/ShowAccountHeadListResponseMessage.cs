using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.AccountHeadMessages
{
  public class ShowAccountHeadListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<AccountHeadMessageModel> AccountHeads { get; }

    public ShowAccountHeadListResponseMessage(ValidationResult validationResult,
      IEnumerable<AccountHeadMessageModel> accountHeads)
    {
      ValidationResult = validationResult;
      AccountHeads = accountHeads;
    }
  }
}