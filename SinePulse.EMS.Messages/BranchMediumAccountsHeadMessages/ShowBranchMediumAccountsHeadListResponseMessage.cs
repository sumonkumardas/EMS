using FluentValidation.Results;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Messages.Model.Accounts;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages
{
  public class ShowBranchMediumAccountsHeadListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<BranchMediumAccountsHeadMessageModel> BranchMediumAccountsHeadList { get; set; }
    public ShowBranchMediumAccountsHeadListResponseMessage(ValidationResult validationResult, List<BranchMediumAccountsHeadMessageModel> branchMediumAccountsHeadList)
    {
      ValidationResult = validationResult;
      BranchMediumAccountsHeadList = branchMediumAccountsHeadList;
    }
  }
}