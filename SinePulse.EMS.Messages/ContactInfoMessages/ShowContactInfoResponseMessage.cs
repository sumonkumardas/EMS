using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Students;

namespace SinePulse.EMS.Messages.ContactInfoMessages
{
  public class ShowContactInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public ContactInfoMessageModel ContactInfo { get; }

    public ShowContactInfoResponseMessage(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    public ShowContactInfoResponseMessage(ValidationResult validationResult, ContactInfoMessageModel contactInfo)
    {
      ValidationResult = validationResult;
      ContactInfo = contactInfo;
    }
  }
}