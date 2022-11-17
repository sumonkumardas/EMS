using FluentValidation;
using SinePulse.EMS.Messages.DesignationMessages;

namespace SinePulse.EMS.UseCases.Designations
{
  public class ShowDesignationRequestMessageValidator : AbstractValidator<ShowDesignationRequestMessage>
  {
    public ShowDesignationRequestMessageValidator()
    {
    }
  }
}