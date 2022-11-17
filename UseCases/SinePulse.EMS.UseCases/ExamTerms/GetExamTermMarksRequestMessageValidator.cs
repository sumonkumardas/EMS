using FluentValidation;
using SinePulse.EMS.Messages.ExamTermMessages;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public class GetExamTermMarksRequestMessageValidator : AbstractValidator<GetExamTermMarksRequestMessage>
  {
    public GetExamTermMarksRequestMessageValidator()
    {
    }
  }
}