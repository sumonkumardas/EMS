using FluentValidation;
using SinePulse.EMS.Messages.OtherComponentMessages;

namespace SinePulse.EMS.UseCases.OtherComponents
{
  public class GetOtherComponentsListRequestMessageValidator : AbstractValidator<GetOtherComponentsListRequestMessage>
  {
    public GetOtherComponentsListRequestMessageValidator()
    {
    }
  }
}