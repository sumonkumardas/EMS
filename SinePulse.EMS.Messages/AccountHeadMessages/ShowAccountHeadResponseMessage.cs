using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.AccountHeadMessages
{
  public class ShowAccountHeadResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public AccountHeadMessageModel AccountHead { get; }

    public ShowAccountHeadResponseMessage(ValidationResult validationResult, AccountHeadMessageModel accountHead)
    {
      ValidationResult = validationResult;
      AccountHead = accountHead;
    }
  }
}