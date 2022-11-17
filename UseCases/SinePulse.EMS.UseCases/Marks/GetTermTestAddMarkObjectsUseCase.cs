using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Marks
{
  public class GetTermTestAddMarkObjectsUseCase : IGetTermTestAddMarkObjectsUseCase
  {
    private readonly IRepository _repository;

    public GetTermTestAddMarkObjectsUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetTermTestAddMarkObjectsResponseMessage.AddMarkObjectsCollection GetAddMarkObjectsCollection(
      GetTermTestAddMarkObjectsRequestMessage message)
    {
      var studentSections = _repository.Table<StudentSection>()
        .Include(nameof(Student))
        .Include(nameof(Section))
        .Where(s => s.Class.Id == message.ClassId &&
                    s.Section.Id == message.SectionId)
        .ToList();
      var requestStudents = ToRequestStudents(studentSections);

      var termTest = _repository.Table<TermTest>()
        .Include(nameof(ExamConfiguration))
        .FirstOrDefault(t => t.ExamConfiguration.Class.Id == message.ClassId &&
                             t.ExamConfiguration.Subject.Id == message.SubjectId &&
                             t.ExamType == (ExamTypeEnum) message.ExamType);
      var requestMark = ToRequestMark(termTest);
      long termTestId = 0;
      if (termTest != null)
        termTestId = termTest.Id;
      return new GetTermTestAddMarkObjectsResponseMessage.AddMarkObjectsCollection
      {
        Students = requestStudents,
        Mark = requestMark,
        TermTestId = termTestId
      };
    }

    private GetTermTestAddMarkObjectsResponseMessage.Mark ToRequestMark(TermTest termTest)
    {
      if (termTest == null) 
        return new GetTermTestAddMarkObjectsResponseMessage.Mark();
      var fullMarks = 0;
      var passMarks = 0;
      switch (termTest.ExamType)
      {
        case ExamTypeEnum.Subjective:
          fullMarks = termTest.ExamConfiguration.SubjectiveFullMark;
          passMarks = termTest.ExamConfiguration.SubjectivePassMark;
          break;
        case ExamTypeEnum.Objective:
          fullMarks = termTest.ExamConfiguration.ObjectiveFullMark;
          passMarks = termTest.ExamConfiguration.ObjectivePassMark;
          break;
        case ExamTypeEnum.Practical:
          fullMarks = termTest.ExamConfiguration.PracticalFullMark;
          passMarks = termTest.ExamConfiguration.PracticalPassMark;
          break;
      }

      return new GetTermTestAddMarkObjectsResponseMessage.Mark
      {
        FullMarks = fullMarks,
        PassMarks = passMarks
      };
    }

    private IEnumerable<GetTermTestAddMarkObjectsResponseMessage.Student> ToRequestStudents(
      List<StudentSection> studentSections)
    {
      var requestStudents = new List<GetTermTestAddMarkObjectsResponseMessage.Student>();
      foreach (var studentSection in studentSections)
      {
        requestStudents.Add(new GetTermTestAddMarkObjectsResponseMessage.Student
        {
          StudentSectionId = studentSection.Id,
          StudentName = studentSection.Student.FullName,
          StudentId = studentSection.Student.Id,
          Roll = studentSection.RollNo
        });
      }

      return requestStudents;
    }
  }
}