using System;
using FluentValidation;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class EditInstituteRequestMessageValidator : AbstractValidator<EditInstituteRequestMessage>
  {
    private readonly IUniqueInstitutePropertyChecker _uniqueInstitutePropertyChecker;
    private readonly IUniqueEmailChecker _uniqueEmailChecker;

    public EditInstituteRequestMessageValidator(IUniqueInstitutePropertyChecker uniqueInstitutePropertyChecker, IUniqueEmailChecker uniqueEmailChecker)
    {
      _uniqueInstitutePropertyChecker = uniqueInstitutePropertyChecker;
      _uniqueEmailChecker = uniqueEmailChecker;

      RuleFor(x => x.OrganisationName).NotEmpty().WithMessage("Please specify Organisation name");
      RuleFor(x => x.OrganisationName).MinimumLength(5).WithMessage("'Organisation Name' is too short, it should have minimum 5 characters");
      RuleFor(x => x.OrganisationName).MaximumLength(200).WithMessage("'Organisation Name' is too long, it can contains at most 200 characters.");
      RuleFor(x => x.OrganisationName).Must(IsUniqueInstituteName).WithMessage("This Institute name already exists.");

      RuleFor(x => x.ShortName).NotEmpty().WithMessage("Please specify Organisation Short name.");
      RuleFor(x => x.ShortName).MinimumLength(3).WithMessage("'Organization Short Name' is too short, it should have minimum 32 characters");
      RuleFor(x => x.ShortName).MaximumLength(32).WithMessage("'Branch Short Name' is too long, it should have minimum 3 characters");
      RuleFor(x => x.ShortName).Matches("[\\w\\d]+").WithMessage(
        "Incorrect Institute Short Name. It can contains only alpha numeric characters");
      RuleFor(x => x.ShortName).Must(IsUniqueInstituteShortName).WithMessage("This Institute name already exists.");

      RuleFor(x => x.Email).NotEmpty().WithMessage(
        "Please specify Organisation email address");
      RuleFor(x => x.Email).EmailAddress().WithMessage(
        "Invalid Email Address. Please enter a valid email address");
      RuleFor(x => x.Email).Must(IsEmailAddressUnique).WithMessage(
        "This Email Address already exist.");
    }

    

    private bool IsUniqueInstituteName(EditInstituteRequestMessage message, string instituteName)
    {
      return _uniqueInstitutePropertyChecker.IsUniqueInstituteName(instituteName, message.Id);
    }

    private bool IsUniqueInstituteShortName(EditInstituteRequestMessage message,string shortName)
    {
      return _uniqueInstitutePropertyChecker.IsUniqueInstituteShortName(shortName, message.Id);
    }

    private bool IsEmailAddressUnique(EditInstituteRequestMessage message, string email)
    {
      return _uniqueEmailChecker.IsUniqueInstitueEmail(email,message.Id);
    }
  }
}