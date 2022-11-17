using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ContactInfoMessages
{
  public class AddContactInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public long ContactInfoId { get; }

    public AddContactInfoResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    public AddContactInfoResponseMessage(ValidationResult validationResult, long contactInfoId)
    {
      ValidationResult = validationResult;
      ContactInfoId = contactInfoId;
    }
  }
}