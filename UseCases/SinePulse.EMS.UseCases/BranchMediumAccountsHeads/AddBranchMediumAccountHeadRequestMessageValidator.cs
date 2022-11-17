using FluentValidation;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
  public class
    AddBranchMediumAccountHeadRequestMessageValidator : AbstractValidator<AddBranchMediumAccountHeadRequestMessage>
  {
        private readonly IUniqueAccountHeadPropertyChecker _uniqueAccountHeadPropertyChecker;

        public AddBranchMediumAccountHeadRequestMessageValidator(IUniqueAccountHeadPropertyChecker uniqueAccountHeadPropertyChecker)
        {
            _uniqueAccountHeadPropertyChecker = uniqueAccountHeadPropertyChecker;
            RuleFor(x => x.AccountCode).NotEmpty().WithMessage("Please Specify Account Code.");
            RuleFor(x => x.AccountCode).NotNull().WithMessage("Please Specify Account Code.");
            RuleFor(x => x.AccountCode).NotEmpty().WithMessage("Please Specify Account Code.");
            RuleFor(x => x.AccountCode).Length(3, 200)
                .WithMessage("Account Code must be minimum 3 character and maximum 200 character.");
            RuleFor(x => x.AccountCode).Must((m, x) => IsUniqueAccountCode(x, m.BranchMediumId,m.CurrentSessionId)).WithMessage("Account Code already exists.");

            RuleFor(x => x.AccountHeadName).NotEmpty().WithMessage("Please Specify Account Head Name.");
            RuleFor(x => x.AccountHeadName).NotNull().WithMessage("Please Specify Account Head Name.");
            RuleFor(x => x.AccountHeadName).NotEmpty().WithMessage("Please Specify Account Head Name.");
            RuleFor(x => x.AccountHeadName).Length(3, 200)
                .WithMessage("Account Head Name must be minimum 3 character and maximum 200 character.");
            RuleFor(x => x.AccountHeadName).Must((m, x) => IsUniqueAccountHeadName(x, m.BranchMediumId, m.CurrentSessionId)).WithMessage("Account Head Name already exists.");
        }

        private bool IsUniqueAccountHeadName(string accountHeadName,long branchMediumId,long sessionId)
        {
            return _uniqueAccountHeadPropertyChecker.IsUniqueAccountHeadNameOfBranch(accountHeadName, branchMediumId, sessionId);
        }

        private bool IsUniqueAccountCode(string accountCode, long branchMediumId, long sessionId)
        {
            return _uniqueAccountHeadPropertyChecker.IsUniqueAccountCodeOfBranch(accountCode, branchMediumId, sessionId);
        }
    }
}