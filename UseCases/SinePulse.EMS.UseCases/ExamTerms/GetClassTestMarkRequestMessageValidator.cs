using FluentValidation;
using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public class GetClassTestMarkRequestMessageValidator : AbstractValidator<GetClassTestMarksRequestMessage>
  {
    public GetClassTestMarkRequestMessageValidator()
    {
    }
  }
}