using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ExamTermMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public class GetExamTermsUseCase : IGetExamTermsUseCase
  {
    private readonly IRepository _repository;

    public GetExamTermsUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public IEnumerable<GetExamTermsResponseMessage.ExamTerm> GetExamTerms(GetExamTermsRequestMessage requestMessage)
    {
      var examTerms = _repository.Table<ExamTerm>()
        .Where(e => e.Session.Id == requestMessage.SessionId &&
                    e.Session.BranchMedium.Id == requestMessage.BranchMediumId)
        .ToList();

      return ToRequestExamTerms(examTerms);
    }

    private IEnumerable<GetExamTermsResponseMessage.ExamTerm> ToRequestExamTerms(List<ExamTerm> examTerms)
    {
      if (examTerms == null) 
        return new List<GetExamTermsResponseMessage.ExamTerm>();
      var requestExamTerms = new List<GetExamTermsResponseMessage.ExamTerm>();
      foreach (var examTerm in examTerms)
      {
        requestExamTerms.Add(new GetExamTermsResponseMessage.ExamTerm
        {
          ExamTermId = examTerm.Id,
          ExamTermName = examTerm.TermName
        });
      }

      return requestExamTerms;
    }
  }
}