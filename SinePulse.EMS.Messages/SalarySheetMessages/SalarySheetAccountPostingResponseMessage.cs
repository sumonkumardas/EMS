using FluentValidation.Results;

namespace SinePulse.EMS.Messages.SalarySheetMessages
{
  public class SalarySheetAccountPostingResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public AccountPostResponse AccountPostResponseData { get;}

    public SalarySheetAccountPostingResponseMessage(ValidationResult validationResult, AccountPostResponse accountPostResponseData)
    {
      ValidationResult = validationResult;
      AccountPostResponseData = accountPostResponseData;
    }
    public class AccountPostResponse
    {
      public bool AccountAlreadyPosted { get; set; }
      public bool AccountPostingSuccessful { get; set; }
      public bool AccountPostingFailed { get; set; }
      public bool ImportChartOfAccounts { get; set; }
    }
  }
}