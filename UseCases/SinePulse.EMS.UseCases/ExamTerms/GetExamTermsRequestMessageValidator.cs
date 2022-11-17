using FluentValidation;
using SinePulse.EMS.Messages.ExamTermMessages;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public class GetExamTermsRequestMessageValidator : AbstractValidator<GetExamTermsRequestMessage>
  {
    public GetExamTermsRequestMessageValidator()
    {
    }
  }
}