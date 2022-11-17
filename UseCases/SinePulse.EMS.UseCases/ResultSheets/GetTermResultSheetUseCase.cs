using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ResultSheetMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ResultSheets
{
  public class GetTermResultSheetUseCase : IGetTermResultSheetUseCase
  {
    private readonly IRepository _repository;

    public GetTermResultSheetUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetTermResultSheetResponseMessage.ExamTermResult GetTermResultSheet(GetTermResultSheetRequestMessage message)
    {
      var resultSheet = _repository.Table<ResultSheet>()
        .Include(nameof(ResultSheet.ResultSheetEntries))
        .Include(nameof(ResultSheet.ResultSheetEntries) + "." + nameof(ExamConfiguration) + "." +
                 nameof(ExamConfiguration.Term))
        .Include(nameof(ResultSheet.ResultSheetEntries) + "." + nameof(ExamConfiguration) + "." + nameof(Subject))
        .FirstOrDefault(r => r.StudentSection.Student.Id == message.StudentId &&
                             r.StudentSection.Student.Session.Id == message.SessionId &&
                             r.TermId == message.ExamTermId);

      var resultSheetEntries = resultSheet?.ResultSheetEntries
        .Where(r => r.ExamConfiguration.Term.Id == message.ExamTermId)
        .ToList();

      return ToResultObject(resultSheet, resultSheetEntries);
    }

    private GetTermResultSheetResponseMessage.ExamTermResult ToResultObject(ResultSheet resultSheet,
      List<ResultSheetEntry> resultSheetEntries)
    {
      if (resultSheet == null || !resultSheetEntries.Any())
        return new GetTermResultSheetResponseMessage.ExamTermResult();
      var requestResultObject = new GetTermResultSheetResponseMessage.ExamTermResult
      {
        GradeLetter = resultSheet.GradeLetter,
        Gpa = resultSheet.Gpa,
        TotalMark = resultSheet.TotalMark
      };
      var subjectWiseResult = new List<GetTermResultSheetResponseMessage.SubjectWiseResult>();

      foreach (var result in resultSheetEntries)
      {
        subjectWiseResult.Add(new GetTermResultSheetResponseMessage.SubjectWiseResult
        {
          SubjectName = result.ExamConfiguration.Subject.SubjectName,
          ClassTestMark = result.ClassTestMark,
          GradeLetter = result.GradeLetter,
          GradePoint = result.GradePoint,
          ObjectiveMark = result.ObjectiveMark,
          PracticalMark = result.PracticalMark,
          SubjectiveMark = result.SubjectiveMark,
          TotalMark = result.TotalMark
        });
      }

      requestResultObject.SubjectWiseResults = subjectWiseResult;
      return requestResultObject;
    }
  }
}