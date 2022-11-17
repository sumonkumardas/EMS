using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.ExamTermMessages
{
  public class GetExamTermsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<ExamTerm> ExamTerms { get; }

    public GetExamTermsResponseMessage(ValidationResult validationResult, IEnumerable<ExamTerm> examTerms)
    {
      ValidationResult = validationResult;
      ExamTerms = examTerms;
    }

    public class ExamTerm
    {
      public long ExamTermId { get; set; }
      public string ExamTermName { get; set; }
    }
  }
}