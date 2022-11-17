using FluentValidation;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Banks
{
  public class EditBankRequestMessageValidator : AbstractValidator<EditBankRequestMessage>
  {
    private readonly IUniqueBankInfoPropertyChecker _uniqueBankInfoPropertyChecker;

    public EditBankRequestMessageValidator(IUniqueBankInfoPropertyChecker uniqueBankInfoPropertyChecker)
    {
      _uniqueBankInfoPropertyChecker = uniqueBankInfoPropertyChecker;
      RuleFor(x => x.BankName).NotEmpty().WithMessage("Please specify Bank Name.");
      RuleFor(x => x.BankName).NotNull().WithMessage("Please specify Bank Name.");
      RuleFor(x => x.BankName).Must((m, x) => IsBankNameExists(m.BankName, m.BankId))
        .WithMessage("Bank Name already exists.");
    }

    private bool IsBankNameExists(string bankName, long bankId)
    {
      return _uniqueBankInfoPropertyChecker.IsUniqueBankName(bankName, bankId);
    }
  }
}