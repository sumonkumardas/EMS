using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.TermTestMarkMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.TermTestMarks
{
  public class GetTermTestMarksUseCase : IGetTermTestMarksUseCase
  {
    private readonly IRepository _repository;

    public GetTermTestMarksUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetTermTestMarksResponseMessage.TermTestMarks GetTermTestMarks(GetTermTestMarksRequestMessage message)
    {
      var termTestMarks = _repository.Table<TermTestMark>()
        .Include(nameof(StudentSection) +"."+ nameof(Class))
        .Include(nameof(StudentSection) +"."+ nameof(Section))
        .Include(nameof(StudentSection) +"."+ nameof(Student))
        .Include(nameof(TermTest) + "." + nameof(ExamConfiguration))
        .Where(x => x.StudentSection.Class.Id == message.ClassId &&
                    x.StudentSection.Section.Id == message.SectionId &&
                    x.TermTest.ExamType == message.ExamType &&
                    x.TermTest.ExamConfiguration.Subject.Id == message.SubjectId);
      var requestStudentMarks = ToRequestStudents(termTestMarks);
      var requestMarks = ToRequestMarks(termTestMarks, message.ExamType);
      return new GetTermTestMarksResponseMessage.TermTestMarks
      {
        StudentMarks = requestStudentMarks,
        Marks = requestMarks,
        ClassId = message.ClassId,          
        SectionId = message.SectionId,
        ExamType = message.ExamType,
        Group = message.Group,
        SubjectId = message.SubjectId
      };
    }

    private GetTermTestMarksResponseMessage.Marks ToRequestMarks(IQueryable<TermTestMark> termTestMarks,
      ExamTypeEnum examType)
    {
      int fullMarks = 0;
      int passMarks = 0;
      var termTest = termTestMarks.Select(x => x.TermTest).FirstOrDefault();
      switch (examType)
      {
        case ExamTypeEnum.Subjective:
          if (termTest != null) fullMarks = termTest.ExamConfiguration.SubjectiveFullMark;
          if (termTest != null) passMarks = termTest.ExamConfiguration.SubjectivePassMark;
          break;
        case ExamTypeEnum.Objective:
          if (termTest != null) fullMarks = termTest.ExamConfiguration.ObjectiveFullMark;
          if (termTest != null) passMarks = termTest.ExamConfiguration.ObjectivePassMark; 
          break;
        case ExamTypeEnum.Practical:
          if (termTest != null) fullMarks = termTest.ExamConfiguration.PracticalFullMark;
          if (termTest != null) passMarks = termTest.ExamConfiguration.PracticalPassMark;
          break;
      }

      long termTestId = 0;
      if (termTest != null)
      {
          termTestId= termTest.Id;
      }

      return new GetTermTestMarksResponseMessage.Marks
      {
        FullMarks = fullMarks,
        PassMarks = passMarks,
        TermTestId = termTestId
      };
    }

    private IEnumerable<GetTermTestMarksResponseMessage.StudentMark> ToRequestStudents(IQueryable<TermTestMark> termTestMarks)
    {
      var studentMarks = new List<GetTermTestMarksResponseMessage.StudentMark>();
      var termTestMarksList = termTestMarks.ToList();
      var studentSections = termTestMarksList.Select(x => x.StudentSection).ToList();
      var index = -1;
      foreach (var studentSection in studentSections)
      {
        index++;
        studentMarks.Add(new GetTermTestMarksResponseMessage.StudentMark
        {
          GraceMark = termTestMarksList[index].GraceMark,
          ObtainedMark = termTestMarksList[index].ObtainedMark,
          Roll = studentSection.RollNo,
          StudentId = studentSection.Student.Id,
          StudentName = studentSection.Student.FullName,
          StudentSectionId = studentSection.Id,
          Remarks = termTestMarksList[index].Remarks
        });
      }

      return studentMarks;
    }
  }
}