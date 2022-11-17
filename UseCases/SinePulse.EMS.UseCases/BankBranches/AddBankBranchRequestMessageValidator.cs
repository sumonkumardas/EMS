using FluentValidation;
using SinePulse.EMS.Messages.BankBranchMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public class AddBankBranchRequestMessageValidator : AbstractValidator<AddBankBranchRequestMessage>
  {
    private readonly IUniqueBankInfoPropertyChecker _uniqueBankInfoPropertyChecker;

    public AddBankBranchRequestMessageValidator(IUniqueBankInfoPropertyChecker uniqueBankInfoPropertyChecker)
    {
      _uniqueBankInfoPropertyChecker = uniqueBankInfoPropertyChecker;
      RuleFor(x => x.BranchName).NotEmpty().WithMessage("Please specify Branch Name.");
      RuleFor(x => x.BranchName).NotNull().WithMessage("Please specify Branch Name.");
      RuleFor(x => x.BranchName).Must(IsBranchNameExists).WithMessage("Branch Name already exists.");
      
      RuleFor(x => x.Address).NotEmpty().WithMessage("Please specify Branch Address.");
      RuleFor(x => x.Address).NotNull().WithMessage("Please specify Branch Address.");
    }

    private bool IsBranchNameExists(string branchName)
    {
      return _uniqueBankInfoPropertyChecker.IsUniqueBankBranchName(branchName);
    }
  }
}