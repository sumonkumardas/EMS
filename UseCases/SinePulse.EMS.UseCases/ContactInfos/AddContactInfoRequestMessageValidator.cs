using FluentValidation;
using SinePulse.EMS.Messages.ContactInfoMessages;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public class AddContactInfoRequestMessageValidator : AbstractValidator<AddContactInfoRequestMessage>
  {
    public AddContactInfoRequestMessageValidator()
    {
      RuleFor(x => x.PhoneNo).NotEmpty().WithMessage("Please Specify Phone Number.");
      RuleFor(x => x.PhoneNo).NotNull().WithMessage("Please Specify Phone Number.");
      RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Enter Valid Email Address.");
    }
  }
}