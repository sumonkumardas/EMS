using System;
using FluentValidation;
using SinePulse.EMS.Messages.CommitteeMemberMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public class AddCommitteeMemberRequestMessageValidator : AbstractValidator<AddCommitteeMemberRequestMessage>
  {
    private readonly IUniqueCommitteeMemberPropertyChecker _uniqueCommitteeMemberPropertyChecker;

    public AddCommitteeMemberRequestMessageValidator(
      IUniqueCommitteeMemberPropertyChecker uniqueCommitteeMemberPropertyChecker)
    {
      _uniqueCommitteeMemberPropertyChecker = uniqueCommitteeMemberPropertyChecker;
      RuleFor(x => x.FullName).NotEmpty().WithMessage("Please specify Full name");
      RuleFor(x => x.FullName).MaximumLength(200).WithMessage("Full name is too long.");
      RuleFor(x => x.FullName).NotNull().WithMessage("Please specify Full name");

      RuleFor(x => x.DOB).NotEmpty().WithMessage("Please specify Date of Birth");
      RuleFor(x => x.DOB).NotNull().WithMessage("Please specify Date of Birth");
      RuleFor(x => x.DOB).LessThan(p => DateTime.Now).WithMessage("Please specify a valid Date of Birth");

      RuleFor(x => x.MobileNo).NotEmpty().WithMessage("Please specify Mobile No.");
      RuleFor(x => x.MobileNo).NotNull().WithMessage("Please specify Mobile No.");
      RuleFor(x => x.MobileNo).Must(IsUniqueMobileNo).WithMessage("Mobile No. already exists.");

      RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Please specify a valid email address.");
      RuleFor(x => x.EmailAddress).Must(IsUniqueEmailAddress).WithMessage("Email Address already exists.");
    }

    private bool IsUniqueEmailAddress(string emailAddress)
    {
      return _uniqueCommitteeMemberPropertyChecker.IsUniqueEmailAddress(emailAddress);
    }

    private bool IsUniqueMobileNo(string mobileNo)
    {
      return _uniqueCommitteeMemberPropertyChecker.IsUniqueMobileNo(mobileNo);
    }
  }
}