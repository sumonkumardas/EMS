using FluentValidation;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Branches
{
  public class EditBranchRequestMessageValidator : AbstractValidator<EditBranchRequestMessage>
  {
    private readonly IUniqueBranchPropertyChecker _uniqueBranchPropertyChecker;
    private readonly IUniqueEmailChecker _uniqueEmailChecker;

    public EditBranchRequestMessageValidator(IUniqueBranchPropertyChecker uniqueBranchPropertyChecker,
      IUniqueEmailChecker uniqueEmailChecker)
    {
      _uniqueBranchPropertyChecker = uniqueBranchPropertyChecker;
      _uniqueEmailChecker = uniqueEmailChecker;

      RuleFor(_ => _.BranchName).NotEmpty().WithMessage(
        "Please specify a Branch Name");
      RuleFor(_ => _.BranchName).MinimumLength(5).WithMessage(
        "'Branch Name' is too short, it should have minimum 5 characters");
      RuleFor(_ => _.BranchName).MaximumLength(200).WithMessage(
        "'Branch Name' is too long, it can contains at most 200 characters");
      RuleFor(_ => _.BranchName).Must(IsBranchNameUnique).WithMessage(
        "This Branch Name already exist.");
      RuleFor(_ => _.BranchCode).NotEmpty().WithMessage(
        "Please specify a Branch Code");
      RuleFor(_ => _.BranchCode).MinimumLength(3).WithMessage(
        "'Branch Code' is too short, it should have minimum 3 characters");
      RuleFor(_ => _.BranchCode).MaximumLength(32).WithMessage(
        "'Branch Code' is too long, it can contains at most 32 characters");
      RuleFor(_ => _.BranchCode).Must(IsBranchCodeUnique).WithMessage(
        "This Branch Code already exist.");
      RuleFor(_ => _.BranchCode).Matches("[\\w\\d]+").WithMessage(
        "Incorrect Branch Code. It can contains only alpha numeric characters");
      RuleFor(_ => _.Email).EmailAddress().WithMessage(
        "Invalid Email Address. Please enter a valid email address");
      RuleFor(_ => _.Email).Must(IsEmailAddressUnique).WithMessage(
        "This Email Address already exist.");
    }

    private bool IsBranchNameUnique(EditBranchRequestMessage message,string branchName)
    {
      return _uniqueBranchPropertyChecker.IsUniqueBranchName(branchName,message.Id);
    }

    private bool IsBranchCodeUnique(EditBranchRequestMessage message, string branchCode)
    {
      return _uniqueBranchPropertyChecker.IsUniqueBranchCode(branchCode, message.Id);
    }

    private bool IsEmailAddressUnique(EditBranchRequestMessage message, string email)
    {
      return _uniqueEmailChecker.IsUniqueBranchEmail(email,message.Id);
    }
  }
}