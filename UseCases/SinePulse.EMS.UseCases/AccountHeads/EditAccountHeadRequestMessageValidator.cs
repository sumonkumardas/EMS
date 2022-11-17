using FluentValidation;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class EditAccountHeadRequestMessageValidator : AbstractValidator<EditAccountHeadRequestMessage>
  {
    private readonly IUniqueAccountHeadPropertyChecker _uniqueAccountHeadPropertyChecker;

    public EditAccountHeadRequestMessageValidator(IUniqueAccountHeadPropertyChecker uniqueAccountHeadPropertyChecker)
    {
      _uniqueAccountHeadPropertyChecker = uniqueAccountHeadPropertyChecker;
      RuleFor(x => x.AccountCode).NotEmpty().WithMessage("Please Specify Account Code.");
      RuleFor(x => x.AccountCode).NotNull().WithMessage("Please Specify Account Code.");
      RuleFor(x => x.AccountCode).NotEmpty().WithMessage("Please Specify Account Code.");
      RuleFor(x => x.AccountCode).Length(3, 200)
        .WithMessage("Account Code must be minimum 3 character and maximum 200 character.");
      RuleFor(x => x).Must((m, x) => IsUniqueAccountCode(x.AccountCode, x.AccountHeadId)).WithMessage("Account Code already exists.");

      RuleFor(x => x.AccountHeadName).NotEmpty().WithMessage("Please Specify Account Head Name.");
      RuleFor(x => x.AccountHeadName).NotNull().WithMessage("Please Specify Account Head Name.");
      RuleFor(x => x.AccountHeadName).NotEmpty().WithMessage("Please Specify Account Head Name.");
      RuleFor(x => x.AccountHeadName).Length(3, 200)
        .WithMessage("Account Head Name must be minimum 3 character and maximum 200 character.");
      RuleFor(x => x).Must((m, x) => IsUniqueAccountHeadName(x.AccountHeadName, x.AccountHeadId)).WithMessage("Account Head Name already exists.");
    }

    private bool IsUniqueAccountHeadName(string accountHeadName, long accountHeadId)
    {
      return _uniqueAccountHeadPropertyChecker.IsUniqueAccountHeadName(accountHeadName, accountHeadId);
    }

    private bool IsUniqueAccountCode(string accountCode, long accountHeadId)
    {
      return _uniqueAccountHeadPropertyChecker.IsUniqueAccountCode(accountCode, accountHeadId);
    }
  }
}