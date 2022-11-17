using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ContactInfoMessages
{
  public class AddPermanentAddressInContactInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ContactInfoId { get; set; }

    public AddPermanentAddressInContactInfoResponseMessage(ValidationResult validationResult, long contactInfoId)
    {
      ValidationResult = validationResult;
      ContactInfoId = contactInfoId;
    }

    public AddPermanentAddressInContactInfoResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}