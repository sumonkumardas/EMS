using FluentValidation;
using SinePulse.EMS.Messages.ContactInfoMessages;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public class AddPresentAddressInContactInfoRequestMessageValidator : AbstractValidator<AddPresentAddressInContactInfoRequestMessage>
  {
    public AddPresentAddressInContactInfoRequestMessageValidator()
    {
      RuleFor(x => x.Street1).NotEmpty().WithMessage("Please Specify Street");
      RuleFor(x => x.Street1).NotNull().WithMessage("Please Specify Street");
      
      RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Please Specify Postal Code");
      RuleFor(x => x.PostalCode).NotNull().WithMessage("Please Specify Postal Code");
      
      RuleFor(x => x.City).NotEmpty().WithMessage("Please Specify City");
      RuleFor(x => x.City).NotNull().WithMessage("Please Specify City");
    }
  }
}