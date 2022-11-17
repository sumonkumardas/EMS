using System.Collections.Generic;
using SinePulse.EMS.Messages.ExamTermMessages;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public interface IGetExamTermsUseCase
  {
    IEnumerable<GetExamTermsResponseMessage.ExamTerm> GetExamTerms(GetExamTermsRequestMessage requestMessage);
  }
}