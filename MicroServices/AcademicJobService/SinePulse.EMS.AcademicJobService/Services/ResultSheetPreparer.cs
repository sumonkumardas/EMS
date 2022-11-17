using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.AcademicJobService.Services
{
  public class ResultSheetPreparer : IResultSheetPreparer
  {
    private readonly IRepository _repository;

    public ResultSheetPreparer(IRepository repository)
    {
      _repository = repository;
    }

    public async Task PrepareResultSheet(long studentSectionId, long termId)
    {
      var examConfigurations = await _repository.Filter<ExamConfiguration>(x => x.Term.Id == termId).ToListAsync();
      var resultGrades = await GetResultGrades(studentSectionId);

      var resultSheet = new ResultSheet
      {
        TermId = termId,
        StudentSectionId = studentSectionId,
        Status = StatusEnum.Active,
        AuditFields =
        {
          InsertedBy = "BatchJob",
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = "BatchJob",
          LastUpdatedDateTime = DateTime.Now
        }
      };

      var resultSheetEntries = new List<ResultSheetEntry>();
      foreach (var examConfiguration in examConfigurations)
      {
        var resultSheetEntry = new ResultSheetEntry
        {
          Status = StatusEnum.Active,
          ResultSheet = resultSheet,
          ExamConfiguration = examConfiguration,
          AuditFields =
          {
            InsertedBy = "BatchJob",
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = "BatchJob",
            LastUpdatedDateTime = DateTime.Now
          }
        };
        await UpdateTermTestMarks(resultSheetEntry, examConfiguration);

        await UpdateClassTestMark(resultSheetEntry, examConfiguration);

        UpdateTotalMark(resultSheetEntry, examConfiguration);

        UpdateGradePoint(resultSheetEntry, resultGrades);

        await _repository.InsertAsync(resultSheetEntry);

        resultSheetEntries.Add(resultSheetEntry);
      }


      UpdateResultSheet(resultSheet, resultSheetEntries, resultGrades);

      await _repository.InsertAsync(resultSheet);
    }

    private async Task<IList<ResultGrade>> GetResultGrades(long studentSectionId)
    {
      var studentSection = await _repository
        .Filter<StudentSection, Section>(x => x.Id == studentSectionId, x => x.Section).FirstAsync();
      var section = await _repository
        .Filter<Section, BranchMedium>(x => x.Id == studentSection.Section.Id, x => x.BranchMedium).FirstAsync();
      return await _repository.Filter<ResultGrade>(x => x.BranchMedium.Id == section.BranchMedium.Id)
        .ToListAsync();
    }

    private async Task UpdateTermTestMarks(ResultSheetEntry resultSheetEntry, ExamConfiguration examConfiguration)
    {
      var termTestMarks = await _repository
        .Filter<ClassTestMark, ClassTest>(x => x.ClassTest.ExamConfiguration.Id == examConfiguration.Id, x => x.ClassTest)
        .ToListAsync();
      foreach (var termTestMark in termTestMarks)
      {
        var testMark = termTestMark.ObtainedMark + termTestMark.GraceMark;
        switch (termTestMark.ClassTest.ExamType)
        {
          case ExamTypeEnum.Subjective:
            resultSheetEntry.SubjectiveMark = testMark;
            break;
          case ExamTypeEnum.Objective:
            resultSheetEntry.ObjectiveMark = testMark;
            break;
          case ExamTypeEnum.Practical:
            resultSheetEntry.ObjectiveMark = testMark;
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private async Task UpdateClassTestMark(ResultSheetEntry resultSheetEntry, ExamConfiguration examConfiguration)
    {
      var classTestMarks = await _repository
        .Filter<ClassTestMark, ClassTest>(
          x => x.ClassTest.ExamConfiguration.Id == examConfiguration.Id,
          x => x.ClassTest)
        .ToListAsync();

      resultSheetEntry.ClassTestMark = classTestMarks.Average(x =>
        (x.ObtainedMark + x.GraceMark) / x.ClassTest.FullMarks * examConfiguration.ClassTestPercentage);
    }

    private static void UpdateTotalMark(ResultSheetEntry resultSheetEntry, ExamConfiguration examConfiguration)
    {
      resultSheetEntry.TotalMark = CalculateTotalMark(resultSheetEntry.SubjectiveMark, resultSheetEntry.ObjectiveMark,
        resultSheetEntry.PracticalMark, resultSheetEntry.ClassTestMark, examConfiguration.ClassTestPercentage);
    }

    private static decimal CalculateTotalMark(decimal subjectiveMark, decimal objectiveMark, decimal practicalMark,
      decimal classTestMark, int classTestPercentage)
    {
      var termTestMark = subjectiveMark + objectiveMark + practicalMark;

      var termTestPercentage = 100 - classTestPercentage;

      var totalMark = termTestPercentage == 100
        ? termTestMark
        : termTestMark * (termTestPercentage / 100m) + classTestMark;

      return totalMark;
    }

    private void UpdateGradePoint(ResultSheetEntry resultSheetEntry, IList<ResultGrade> grades)
    {
      var grade = GetGradeFromMark(grades, resultSheetEntry.TotalMark);
      resultSheetEntry.GradePoint = grade.GradePoint;
      resultSheetEntry.GradeLetter = grade.GradeLetter;
    }

    private void UpdateResultSheet(ResultSheet resultSheet, IList<ResultSheetEntry> resultSheetEntries,
      IList<ResultGrade> grades)
    {
      resultSheet.TotalMark = resultSheetEntries.Sum(x => x.TotalMark);
      resultSheet.Gpa = resultSheetEntries.Average(x => x.GradePoint);
      resultSheet.GradeLetter = GetGradeFromGpa(grades, resultSheet.Gpa).GradeLetter;
    }

    private ResultGrade GetGradeFromMark(ICollection<ResultGrade> grades, decimal mark)
    {
      return grades.First(g => g.StartMark <= mark && mark <= g.EndMark);
    }

    private ResultGrade GetGradeFromGpa(ICollection<ResultGrade> grades, decimal gpa)
    {
      return grades.OrderBy(g => g.GradePoint).First(g => g.GradePoint <= gpa);
    }
  }
}