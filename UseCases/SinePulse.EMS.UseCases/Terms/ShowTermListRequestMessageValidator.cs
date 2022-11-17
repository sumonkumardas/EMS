using FluentValidation;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Terms
{
  public class ShowTermListRequestMessageValidator : AbstractValidator<ShowTermListRequestMessage>
  {

    public ShowTermListRequestMessageValidator()
    {
    }
  }
}