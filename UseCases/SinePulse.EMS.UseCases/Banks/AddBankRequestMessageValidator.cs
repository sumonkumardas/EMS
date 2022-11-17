using FluentValidation;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Banks
{
  public class AddBankRequestMessageValidator : AbstractValidator<AddBankRequestMessage>
  {
    private readonly IUniqueBankInfoPropertyChecker _uniqueBankInfoPropertyChecker;

    public AddBankRequestMessageValidator(IUniqueBankInfoPropertyChecker uniqueBankInfoPropertyChecker)
    {
      _uniqueBankInfoPropertyChecker = uniqueBankInfoPropertyChecker;
      RuleFor(x => x.BankName).NotEmpty().WithMessage("Please specify Bank Name.");
      RuleFor(x => x.BankName).NotNull().WithMessage("Please specify Bank Name.");
      RuleFor(x => x.BankName).Must(IsBankNameExists).WithMessage("Bank Name already exists.");
    }

    private bool IsBankNameExists(string bankName)
    {
      return _uniqueBankInfoPropertyChecker.IsUniqueBankName(bankName);
    }
  }
}