using FluentValidation;
using SinePulse.EMS.Messages.TermTestMarkMessages;

namespace SinePulse.EMS.UseCases.TermTestMarks
{
  public class GetTermTestMarksRequestMessageValidator : AbstractValidator<GetTermTestMarksRequestMessage>
  {
    public GetTermTestMarksRequestMessageValidator()
    {
    }
  }
}