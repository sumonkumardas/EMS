using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class ShowClassTestListUseCase : IShowClassTestListUseCase
  {
    private readonly IRepository _repository;

    public ShowClassTestListUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowClassTestListResponseMessage ShowClassTestList(ShowClassTestListRequestMessage message)
    {
      var includes = new[]
      {
          nameof(ClassTest.Section),
          $"{nameof(ClassTest.Section)}.{nameof(Section.Class)}",
          nameof(ClassTest.ExamConfiguration),
          $"{nameof(ClassTest.ExamConfiguration)}.{nameof(ExamConfiguration.Class)}",
          $"{nameof(ClassTest.ExamConfiguration)}.{nameof(ExamConfiguration.Subject)}"
      };
      var classTestList = _repository.Table<ClassTest>(includes).Where(x=>x.Section !=null && x.Section.Id == message.SectionId).ToList();
      return new ShowClassTestListResponseMessage(ClassTestEntity(classTestList));
    }

    private static List<ShowClassTestListResponseMessage.ClassTest> ClassTestEntity(IEnumerable<ClassTest> testList)
    {
        var list = new List<ShowClassTestListResponseMessage.ClassTest>();
        foreach (var classTest in testList)
        {
            var ct = new ShowClassTestListResponseMessage.ClassTest
            {
                TestId = classTest.Id,
                StartTimestamp = classTest.StartTimestamp,
                EndTimestamp = classTest.EndTimestamp,
                TestName = classTest.TestName
            };

            list.Add(ct);
        }

        return list;
    }
  }
}