using FluentValidation;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.BillingMessages;
using SinePulse.EMS.Messages.OnlinePaymentMessages;
using SinePulse.EMS.UseCases.Billings;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class AddPaymentRequestMessageValidator : AbstractValidator<AddPaymentRequestMessage>
  {
    private readonly IGetUnpaidMonthsUseCase _getUnpaidMonthsUseCase;

    public AddPaymentRequestMessageValidator(IGetUnpaidMonthsUseCase getUnpaidMonthsUseCase)
    {
      _getUnpaidMonthsUseCase = getUnpaidMonthsUseCase;
      RuleFor(x => x.DueAmount).GreaterThan(0).WithMessage("Invalid Amount");
      RuleFor(x => x.Month).Must(IsValidMonth).WithMessage("Select Month");
      RuleFor(x => x.Year).GreaterThan(0).WithMessage("Select Year");
      RuleFor(x => x.UserCode).Must((m, x) => IsValidUser(m.UserCode)).WithMessage("User not found");
      RuleFor(x => x.TransactionId).Must((m, x) => IsValidTransactionId(m.TransactionId))
        .WithMessage("Transaction Id generation failed");
      RuleFor(x => x.Month).Must((m, x) => HasPreviousDue(m.Month, m.Year, m.BranchMediumId))
        .WithMessage("You have previous due. At first clear previous months due.");
    }

    private bool HasPreviousDue(MonthsOfYearEnum month, int year, long branchMediumId)
    {
      var unpaidMonths = _getUnpaidMonthsUseCase.GetUnpaidMonths(new GetUnpaidMonthsRequestMessage
        {BranchMediumId = branchMediumId, Year = year});
      foreach (var unpaidMonth in unpaidMonths)
      {
        if ((int) unpaidMonth.MonthEnum < (int) month)
          return false;
      }

      return true;
    }

    private bool IsValidTransactionId(string transactionId)
    {
      if (transactionId == null)
        return false;
      return true;
    }

    private bool IsValidUser(string userCode)
    {
      if (userCode == null)
        return false;
      return true;
    }

    private bool IsValidMonth(MonthsOfYearEnum month)
    {
      if ((int) month == 0 || (int) month > 12)
      {
        return false;
      }

      return true;
    }
  }
}