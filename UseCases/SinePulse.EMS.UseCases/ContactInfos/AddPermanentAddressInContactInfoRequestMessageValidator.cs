using FluentValidation;
using SinePulse.EMS.Messages.ContactInfoMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public class
    AddPermanentAddressInContactInfoRequestMessageValidator : AbstractValidator<
      AddPermanentAddressInContactInfoRequestMessage>
  {
    private readonly IPresentAddressInContactInfoChecker _presentAddressInContactInfoChecker;

    public AddPermanentAddressInContactInfoRequestMessageValidator(
      IPresentAddressInContactInfoChecker presentAddressInContactInfoChecker)
    {
      _presentAddressInContactInfoChecker = presentAddressInContactInfoChecker;
      RuleFor(x => x).Must((m, x) => IsValidStreet1(x.IsSameAsPresentAddress, x.Street1)).WithMessage("Please Specify Street");

      RuleFor(x => x).Must((m, x) => IsValidPostalCode(x.IsSameAsPresentAddress, x.PostalCode)).WithMessage("Please Specify Postal Code");
     
      RuleFor(x => x).Must((m, x) => IsValidCity(x.IsSameAsPresentAddress, x.City)).WithMessage("Please Specify City");

      RuleFor(x => x).Must((m, x) => PresentAddressAdded(x.IsSameAsPresentAddress, x.ContactInfoId))
        .WithMessage("At first add present address.");
    }

    private bool IsValidCity(bool isSameAsPresentAddress, string city)
    {
      if (!isSameAsPresentAddress)
      {
        return !string.IsNullOrEmpty(city);
      }
      return true;
    }

    private bool IsValidPostalCode(bool isSameAsPresentAddress, string postalCode)
    {
      if (!isSameAsPresentAddress)
      {
        return !string.IsNullOrEmpty(postalCode);
      }
      return true;
    }

    private bool IsValidStreet1(bool isSameAsPresentAddress, string street1)
    {
      if (!isSameAsPresentAddress)
      {
        return !string.IsNullOrEmpty(street1);
      }
      return true;
    }

    private bool PresentAddressAdded(bool isSameAsPresentAddress, long contactInfoId)
    {
      if (isSameAsPresentAddress)
      {
        return _presentAddressInContactInfoChecker.IsPresentAddressAdded(contactInfoId);
      }
      return true;
    }
  }
}