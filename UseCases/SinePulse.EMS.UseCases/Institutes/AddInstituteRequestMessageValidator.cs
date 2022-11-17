using FluentValidation;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class AddInstituteRequestMessageValidator : AbstractValidator<AddInstituteRequestMessage>
  {
    private readonly IUniqueInstitutePropertyChecker _uniqueInstitutePropertyChecker;
    private readonly IUniqueEmailChecker _uniqueEmailChecker;

    public AddInstituteRequestMessageValidator(IUniqueInstitutePropertyChecker uniqueInstitutePropertyChecker,IUniqueEmailChecker uniqueEmailChecker)
    {
      _uniqueInstitutePropertyChecker = uniqueInstitutePropertyChecker;
      _uniqueEmailChecker = uniqueEmailChecker;

      RuleFor(x => x.OrganisationName).NotEmpty().WithMessage("Please specify Organisation name");
      RuleFor(x => x.OrganisationName).MinimumLength(5).WithMessage("'Organisation Name' is too short, it should have minimum 5 characters");
      RuleFor(x => x.OrganisationName).MaximumLength(200).WithMessage("'Organisation Name' is too long, it can contains at most 200 characters.");
      RuleFor(x => x.OrganisationName).Must(IsUniqueInstituteName).WithMessage("This Institute name already exists.");

      RuleFor(x => x.ShortName).NotEmpty().WithMessage("Please specify Organisation Short name.");
      RuleFor(x => x.ShortName).MinimumLength(2).WithMessage("'Organization Short Name' is too short, it should have minimum 2 characters");
      RuleFor(x => x.ShortName).MaximumLength(32).WithMessage("'Organization Short Name' is too long, it should have at most 32 characters");
      RuleFor(x => x.ShortName).Must(IsUniqueShortName).WithMessage("This Organization Short name already exists");
      RuleFor(x => x.ShortName).Matches("[\\w\\d]+").WithMessage(
        "Incorrect Institute Short Name. It can contains only alpha numeric characters");

      RuleFor(x => x.Email).EmailAddress().WithMessage(
        "Invalid Email Address. Please enter a valid email address");
      RuleFor(x => x.Email).EmailAddress().WithMessage(
        "Invalid Email Address. Please enter a valid email address");
      RuleFor(x => x.Email).Must(IsEmailAddressUnique).WithMessage(
        "This Email Address already exist.");
      
      RuleFor(x => x.Banner).NotNull().WithMessage(
        "Please Select Banner Image.");
    }

    private bool IsUniqueInstituteName(string instituteName)
    {
      return _uniqueInstitutePropertyChecker.IsUniqueInstituteName(instituteName);
    }

    private bool IsUniqueShortName(string shortName)
    {
      return _uniqueInstitutePropertyChecker.IsUniqueInstituteShortName(shortName);
    }

    private bool IsEmailAddressUnique(string email)
    {
      return _uniqueEmailChecker.IsUniqueEmail(email);
    }
  }
}