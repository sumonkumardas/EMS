using FluentValidation;
using SinePulse.EMS.Messages.BankAccountMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public class EditBankAccountRequestMessageValidator : AbstractValidator<EditBankAccountRequestMessage>
  {
    private readonly IUniqueBankInfoPropertyChecker _uniqueBankInfoPropertyChecker;
    public EditBankAccountRequestMessageValidator(IUniqueBankInfoPropertyChecker uniqueBankInfoPropertyChecker)
    {
      _uniqueBankInfoPropertyChecker = uniqueBankInfoPropertyChecker;
      RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("Please Specify Account Number.");
      RuleFor(x => x.AccountNumber).NotNull().WithMessage("Please Specify Account Number.");
      RuleFor(x => x.AccountNumber).Must((m, x) => IsUniqueBankAccountNumber(m.AccountNumber, m.BankAccountId))
        .WithMessage("Account Number Already Exists.");
    }

    private bool IsUniqueBankAccountNumber(string accountNumber, long bankAccountId)
    {
      return _uniqueBankInfoPropertyChecker.IsUniqueBankAccountNumber(accountNumber, bankAccountId);
    }
  }
}