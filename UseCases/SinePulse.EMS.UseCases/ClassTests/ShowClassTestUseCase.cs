using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class ShowClassTestUseCase : IShowClassTestUseCase
  {
    private readonly IRepository _repository;

    public ShowClassTestUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowClassTestResponseMessage ShowClassTest(ShowClassTestRequestMessage message)
    {
      var includes = new[]
      {
        nameof(ClassTest.Section),
        $"{nameof(ClassTest.Section)}.{nameof(Section.Class)}",
        nameof(ClassTest.ExamConfiguration),
        $"{nameof(ClassTest.ExamConfiguration)}.{nameof(ExamConfiguration.Term)}",
        $"{nameof(ClassTest.ExamConfiguration)}.{nameof(ExamConfiguration.Class)}",
        $"{nameof(ClassTest.ExamConfiguration)}.{nameof(ExamConfiguration.Subject)}",
      };
      var classTest = _repository.GetByIdWithInclude<ClassTest>(message.ClassTestId, includes);
      return new ShowClassTestResponseMessage(ClassTestEntity(classTest));
    }

    private ShowClassTestResponseMessage.ClassTest ClassTestEntity(ClassTest test)
    {
      return new ShowClassTestResponseMessage.ClassTest
      {
        TestId = test.Id,
        TestName = test.TestName,
        FullMarks = test.FullMarks,
        StartTimestamp = test.StartTimestamp,
        EndTimestamp = test.EndTimestamp,
        ExamType = test.ExamType,
        Status = test.Status,
        Term = new ShowClassTestResponseMessage.Term
        {
          TermId = test.ExamConfiguration.Term.Id,
          TermName = test.ExamConfiguration.Term.TermName
        },
        //Group = test.ExamConfiguration.ClassSubject.Group,
        Subject = new ShowClassTestResponseMessage.Subject
        {
          SubjectId = test.ExamConfiguration.Subject.Id,
          SubjectName = test.ExamConfiguration.Subject.SubjectName
        },
        Section = new ShowClassTestResponseMessage.Section
        {
          SectionId = test.Section.Id,
          SectionName = test.Section.SectionName,
          ClassName = test.Section.Class.ClassName
        },
        ExamConfigurationId = test.ExamConfiguration.Id
      };
    }
  }
}