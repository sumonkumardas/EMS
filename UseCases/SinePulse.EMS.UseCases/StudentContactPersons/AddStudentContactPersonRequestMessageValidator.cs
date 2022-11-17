using FluentValidation;
using SinePulse.EMS.Messages.StudentContactPersonMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public class AddStudentContactPersonRequestMessageValidator : AbstractValidator<AddStudentContactPersonRequestMessage>
  {
    private readonly IUniqueEmailChecker _uniqueEmailChecker;
    public AddStudentContactPersonRequestMessageValidator(IUniqueEmailChecker uniqueEmailChecker)
    {
      _uniqueEmailChecker = uniqueEmailChecker;
      RuleFor(x => x.PhoneNo).NotEmpty().WithMessage("Please Specify Phone No.");
      RuleFor(x => x.PhoneNo).NotNull().WithMessage("Please Specify Phone No.");
      RuleFor(x => x.PhoneNo).Must(IsUniquePhoneNo).WithMessage("his Phone Number already exists.");
     
      RuleFor(x => x.Name).NotEmpty().WithMessage("Please Specify Name.");
      RuleFor(x => x.Name).NotNull().WithMessage("Please Specify Name.");
      RuleFor(x => x.Name).MinimumLength(5).WithMessage(
        "'Name' is too short, it should have minimum 5 characters");
      RuleFor(x => x.Name).MaximumLength(200).WithMessage(
        "'Name' is too long, it can contains at most 200 characters");

      RuleFor(x => x.EmailAddress).Must(IsEmailAddressUnique).WithMessage(
        "This Email Address already exists.");
      RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Invalid Email Address.");
    }

    private bool IsUniquePhoneNo(string phoneNo)
    {
      return _uniqueEmailChecker.IsUniquePhoneNo(phoneNo);
    }

    private bool IsEmailAddressUnique(string email)
    {
      return _uniqueEmailChecker.IsUniqueContactPersonEmail(email);
    }
  }
}