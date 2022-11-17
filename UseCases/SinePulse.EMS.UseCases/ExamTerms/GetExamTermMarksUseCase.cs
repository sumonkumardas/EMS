using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.ExamTermMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ExamTerms
{
  public class GetExamTermMarksUseCase : IGetExamTermMarksUseCase
  {
    private readonly IRepository _repository;

    public GetExamTermMarksUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetExamTermMarksResponseMessage.TermMarks GetExamTermMarks(GetExamTermMarksRequestMessage requestMessage)
    {
      var examTermName = _repository.GetById<ExamTerm>(requestMessage.ExamTermId).TermName;
      var requestTermMarks = new GetExamTermMarksResponseMessage.TermMarks {TermName = examTermName};
      var termTestMarks = new List<GetExamTermMarksResponseMessage.TermTestMark>();
      var marks = _repository.Table<TermTestMark>()
        .Include(nameof(StudentSection) + "." + nameof(Class))
        .Include(nameof(StudentSection) + "." + nameof(Section))
        .Include(nameof(StudentSection) + "." + nameof(Student))
        .Include(nameof(TermTest) + "." + nameof(ExamConfiguration) + "." + nameof(Class))
        .Include(nameof(TermTest) + "." + nameof(ExamConfiguration) + "." + nameof(Subject))
        .Include(nameof(TermTest) + "." + nameof(ExamConfiguration) + "." + nameof(ExamConfiguration.Term))
        .Where(x => x.TermTest.ExamConfiguration.Term.Id == requestMessage.ExamTermId &&
                    x.StudentSection.Class.Id == requestMessage.ClassId &&
                    x.StudentSection.Student.Id == requestMessage.StudentId &&
                    x.StudentSection.Section.Id == requestMessage.SectionId)
        .ToList();

      foreach (var mark in marks)
      {
        termTestMarks.Add(new GetExamTermMarksResponseMessage.TermTestMark
        {
          TermTestId = mark.TermTest.Id,
          ExamType = mark.TermTest.ExamType.ToString("G"),
          SubjectName = mark.TermTest.ExamConfiguration.Subject.SubjectName,
          FullMarks = GetFullMarks(mark.TermTest),
          PassMarks = GetPassMarks(mark.TermTest),
          MarkDetails = new GetExamTermMarksResponseMessage.MarkDetails
          {
            GraceMarks = mark.GraceMark,
            ObtainedMarks = mark.ObtainedMark,
            Remarks = mark.Remarks
          }
        });
      }

      requestTermMarks.TermTestMarks = termTestMarks;
      return requestTermMarks;
    }

    private int GetFullMarks(TermTest termTest)
    {
      int fullMarks;
      switch (termTest.ExamType)
      {
        case ExamTypeEnum.Subjective:
          fullMarks = termTest.ExamConfiguration.SubjectiveFullMark;
          break;
        case ExamTypeEnum.Objective:
          fullMarks = termTest.ExamConfiguration.ObjectiveFullMark;
          break;
        case ExamTypeEnum.Practical:
          fullMarks = termTest.ExamConfiguration.PracticalFullMark;
          break;
        default:
          fullMarks = 0;
          break;
      }

      return fullMarks;
    }

    private int GetPassMarks(TermTest termTest)
    {
      int passMarks;
      switch (termTest.ExamType)
      {
        case ExamTypeEnum.Subjective:
          passMarks = termTest.ExamConfiguration.SubjectivePassMark;
          break;
        case ExamTypeEnum.Objective:
          passMarks = termTest.ExamConfiguration.ObjectivePassMark;
          break;
        case ExamTypeEnum.Practical:
          passMarks = termTest.ExamConfiguration.PracticalPassMark;
          break;
        default:
          passMarks = 0;
          break;
      }

      return passMarks;
    }
  }
}