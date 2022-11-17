using FluentValidation;
using SinePulse.EMS.Messages.MediumMessages;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class ShowMediumRequestMessageValidator : AbstractValidator<ShowMediumRequestMessage>
  {
    public ShowMediumRequestMessageValidator()
    {
    }
  }
}