using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class ShowTermTestListUseCase : IShowTermTestListUseCase
  {
    private readonly IRepository _repository;

    public ShowTermTestListUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public IEnumerable<TermTest> ShowTermTestList(ShowTermTestListRequestMessage requestMessage)
    {
      var includes = new string[]
      {
        nameof(TermTest.ExamConfiguration),
        nameof(TermTest.ExamConfiguration) + "." + nameof(TermTest.ExamConfiguration.Subject),
        nameof(TermTest.ExamConfiguration) + "." + nameof(TermTest.ExamConfiguration.Class)
      };
      var classTestList = _repository.Table<TermTest>(includes)
                                     .Where(t => t.ExamConfiguration.Term.Id == requestMessage.TermId);

      if (requestMessage.Day == null || requestMessage.Month == null || requestMessage.Year == null)
        return classTestList;
      var expectedDate =
        new DateTime(requestMessage.Year.Value, requestMessage.Month.Value, requestMessage.Day.Value);

      classTestList = classTestList.Where(x=>x.StartTimestamp.DayOfYear >= expectedDate.DayOfYear && x.EndTimestamp.DayOfYear <= expectedDate.DayOfYear);

      return classTestList;
    }
  }
}