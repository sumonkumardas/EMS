using FluentValidation;
using SinePulse.EMS.Messages.ContactInfoMessages;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public class ShowContactInfoRequestMessageValidator : AbstractValidator<ShowContactInfoRequestMessage>
  {
    public ShowContactInfoRequestMessageValidator()
    {
    }
  }
}