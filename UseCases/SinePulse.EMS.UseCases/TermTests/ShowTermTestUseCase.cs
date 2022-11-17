using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class ShowTermTestUseCase : IShowTermTestUseCase
  {
    private readonly IRepository _repository;

    public ShowTermTestUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public TermTest ShowTermTest(ShowTermTestRequestMessage requestMessage)
    {
      var includes = new string[]
      {
        nameof(TermTest.ExamConfiguration),
        nameof(TermTest.ExamConfiguration) + "." + nameof(TermTest.ExamConfiguration.Term),
        nameof(TermTest.ExamConfiguration) + "." + nameof(TermTest.ExamConfiguration.Subject),
        nameof(TermTest.ExamConfiguration) + "." + nameof(TermTest.ExamConfiguration.Class),
        nameof(TermTest.ExamConfiguration) + "." + nameof(TermTest.ExamConfiguration.Term)+"."+$"{nameof(ExamTerm.Session) +"."+ nameof(BranchMedium)}.{nameof(BranchMedium.Branch)}"
      };
      var classTest = _repository.GetById<TermTest>(requestMessage.TermTestId,includes);
      return classTest;
    }
  }
}