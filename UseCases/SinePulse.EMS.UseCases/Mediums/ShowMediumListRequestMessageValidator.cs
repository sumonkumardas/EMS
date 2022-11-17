using FluentValidation;
using SinePulse.EMS.Messages.MediumMessages;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class ShowMediumListRequestMessageValidator : AbstractValidator<ShowMediumListRequestMessage>
  {
    public ShowMediumListRequestMessageValidator()
    {
    }
  }
}