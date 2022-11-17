using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ContactInfoMessages
{
  public class AddPresentAddressInContactInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ContactInfoId { get; set; }

    public AddPresentAddressInContactInfoResponseMessage(ValidationResult validationResult, long contactInfoId)
    {
      ValidationResult = validationResult;
      ContactInfoId = contactInfoId;
    }

    public AddPresentAddressInContactInfoResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }
  }
}