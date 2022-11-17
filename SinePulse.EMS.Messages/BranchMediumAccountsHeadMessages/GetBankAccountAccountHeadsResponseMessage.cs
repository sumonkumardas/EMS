using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages
{
  public class GetBankAccountAccountHeadsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<BranchMediumAccountsHeadMessageModel> BankAccountAccountHeadsList { get; }

    public GetBankAccountAccountHeadsResponseMessage(ValidationResult validationResult,
      IEnumerable<BranchMediumAccountsHeadMessageModel> bankAccountAccountHeadsList)
    {
      ValidationResult = validationResult;
      BankAccountAccountHeadsList = bankAccountAccountHeadsList;
    }
  }
}