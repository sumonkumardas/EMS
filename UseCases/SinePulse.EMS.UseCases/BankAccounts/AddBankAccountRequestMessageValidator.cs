using FluentValidation;
using SinePulse.EMS.Messages.BankAccountMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.BankAccounts
{
  public class AddBankAccountRequestMessageValidator : AbstractValidator<AddBankAccountRequestMessage>
  {
    private readonly IUniqueBankInfoPropertyChecker _uniqueBankInfoPropertyChecker;
    private readonly IBankAccountAddPropertyChecker _bankAccountAddPropertyChecker;
    public AddBankAccountRequestMessageValidator(IUniqueBankInfoPropertyChecker uniqueBankInfoPropertyChecker, 
      IBankAccountAddPropertyChecker bankAccountAddPropertyChecker)
    {
      _uniqueBankInfoPropertyChecker = uniqueBankInfoPropertyChecker;
      _bankAccountAddPropertyChecker = bankAccountAddPropertyChecker;
      RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("Please Specify Account Number.");
      RuleFor(x => x.AccountNumber).NotNull().WithMessage("Please Specify Account Number.");
      RuleFor(x => x.AccountNumber).Must(IsUniqueBankAccountNumber).WithMessage("Account Number Already Exists.");
            
      RuleFor(x => x).Must((m, x) => IsCurrentSessionExists(m.BankBranchId)).
        WithMessage("Current Session Not Added.");
      
      RuleFor(x => x).Must((m, x) => IsChartOfAccountsImported(m.BankBranchId)).
        WithMessage("Chart of Accounts not imported in Current Session.");
    }

    private bool IsCurrentSessionExists(long bankBranchId)
    {
      return _bankAccountAddPropertyChecker.IsCurrentSessionExists(bankBranchId);
    }

    private bool IsChartOfAccountsImported(long bankBranchId)
    {
      return _bankAccountAddPropertyChecker.IsCharOfAccountImported(bankBranchId);
    }

    private bool IsUniqueBankAccountNumber(string accountNumber)
    {
      return _uniqueBankInfoPropertyChecker.IsUniqueBankAccountNumber(accountNumber);
    }
  }
}