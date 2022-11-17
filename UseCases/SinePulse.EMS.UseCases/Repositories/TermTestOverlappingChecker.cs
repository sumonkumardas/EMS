using System;
using System.Linq;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class TermTestOverlappingChecker : ITermTestOverlappingChecker
  {
    private readonly EmsDbContext _dbContext;
    private readonly IRepository _repository;
    private readonly ITimestampOverlappingChecker _timestampOverlappingChecker;

    public TermTestOverlappingChecker(EmsDbContext dbContext, IRepository repository,
      ITimestampOverlappingChecker timestampOverlappingChecker)
    {
      _dbContext = dbContext;
      _repository = repository;
      _timestampOverlappingChecker = timestampOverlappingChecker;
    }

    public bool IsOnBetweenValidTermDate(DateTime routineDate, long termId)
    {
      var term = _repository.GetById<ExamTerm>(termId);
      return routineDate.DayOfYear >= term.StartDate.DayOfYear && routineDate.DayOfYear <= term.EndDate.DayOfYear;
    }

    public bool IsNonOverlappingStartTime(DateTime routineDate, TimeSpan startTime, long termId, long classId,
      GroupEnum group, long subjectId)
    {
      var examConfiguration = GetExamConfiguration(classId, subjectId, group);
      var includes = new string[]
      {
        nameof(TermTest.ExamConfiguration),
        nameof(TermTest.ExamConfiguration) + "." + nameof(ExamConfiguration.Term),
        nameof(TermTest.ExamConfiguration) + "." + nameof(ExamConfiguration.Class)
      };

      var termTestList = _repository
        .Table<TermTest>(includes).Where(x =>
          x.StartTimestamp.DayOfYear >= routineDate.DayOfYear && x.EndTimestamp.DayOfYear <= routineDate.DayOfYear && x.ExamConfiguration.Class.Id == classId);

      var startDate = routineDate + startTime;
      foreach (var termTest in termTestList)
      {
        if (startDate.TimeOfDay >= termTest.StartTimestamp.TimeOfDay &&
            startDate.TimeOfDay <= termTest.EndTimestamp.TimeOfDay)
          return true;
      }

      return false;
    }

    public bool IsNonOverlappingEndTime(DateTime routineDate, TimeSpan endTime, long termId, long classId,
      GroupEnum group, long subjectId)
    {
      var examConfiguration = GetExamConfiguration(classId, subjectId, group);
      var includes = new string[]
      {
        nameof(TermTest.ExamConfiguration),
        nameof(TermTest.ExamConfiguration) + "." + nameof(ExamConfiguration.Term),
        nameof(TermTest.ExamConfiguration) + "." + nameof(ExamConfiguration.Class)
      };

      var termTestList = _repository
        .Table<TermTest>(includes).Where(x =>
          x.StartTimestamp.DayOfYear >= routineDate.DayOfYear && x.EndTimestamp.DayOfYear <= routineDate.DayOfYear && x.ExamConfiguration.Class.Id == classId);


      var endDate = routineDate + endTime;
      foreach (var termTest in termTestList)
      {
        if (endDate.TimeOfDay >= termTest.StartTimestamp.TimeOfDay &&
            endDate.TimeOfDay <= termTest.EndTimestamp.TimeOfDay)
          return true;
      }

      return false;
    }

    public bool IsNonOverlappingStartTime(DateTime routineDate, TimeSpan startTime, long termId, long classId,
      GroupEnum group, long subjectId, long termTestId)
    {
      var examConfiguration = GetExamConfiguration(classId, subjectId, group);
      var includes = new string[]
      {
        nameof(TermTest.ExamConfiguration),
        nameof(TermTest.ExamConfiguration) + "." + nameof(ExamConfiguration.Term),
        nameof(TermTest.ExamConfiguration) + "." + nameof(ExamConfiguration.Class)
      };

      var termTestList = _repository
        .Table<TermTest>(includes).Where(x => x.Id != termTestId &&
          x.StartTimestamp.DayOfYear >= routineDate.DayOfYear && x.EndTimestamp.DayOfYear <= routineDate.DayOfYear && x.ExamConfiguration.Class.Id == classId);

      var startDate = routineDate + startTime;
      foreach (var termTest in termTestList)
      {
        if (startDate.TimeOfDay >= termTest.StartTimestamp.TimeOfDay &&
            startDate.TimeOfDay <= termTest.EndTimestamp.TimeOfDay)
          return true;
      }

      return false;
    }

    public bool IsNonOverlappingEndTime(DateTime routineDate, TimeSpan endTime, long termId, long classId,
      GroupEnum group, long subjectId, long termTestId)
    {
      var examConfiguration = GetExamConfiguration(classId, subjectId, group);
      var includes = new string[]
      {
        nameof(TermTest.ExamConfiguration),
        nameof(TermTest.ExamConfiguration) + "." + nameof(ExamConfiguration.Term),
        nameof(TermTest.ExamConfiguration) + "." + nameof(ExamConfiguration.Class)
      };

      var termTestList = _repository
        .Table<TermTest>(includes).Where(x => x.Id != termTestId &&
          x.StartTimestamp.DayOfYear >= routineDate.DayOfYear && x.EndTimestamp.DayOfYear <= routineDate.DayOfYear && x.ExamConfiguration.Class.Id == classId);


      var endDate = routineDate + endTime;
      foreach (var termTest in termTestList)
      {
        if (endDate.TimeOfDay >= termTest.StartTimestamp.TimeOfDay &&
            endDate.TimeOfDay <= termTest.EndTimestamp.TimeOfDay)
          return true;
      }

      return false;
    }

    public bool IsTermTestForSubjectAlreadyScheduled(long classId, long subjectId, long termId, ExamTypeEnum examType)
    {
      return !_dbContext.TermTests.Any(t => t.ExamConfiguration.Term.Id == termId &&
                                            t.ExamType == examType &&
                                            t.ExamConfiguration.Class.Id == classId &&
                                            t.ExamConfiguration.Subject.Id == subjectId);
    }
    public bool IsTermTestForSubjectAlreadyScheduled(long classId, long subjectId, long termId, ExamTypeEnum examType, long termTestId)
    {
      return !_dbContext.TermTests.Any(t => t.ExamConfiguration.Term.Id == termId && t.Id != termTestId &&
                                            t.ExamType == examType &&
                                            t.ExamConfiguration.Class.Id == classId &&
                                            t.ExamConfiguration.Subject.Id == subjectId);
    }
    private ExamConfiguration GetExamConfiguration(long classId, long subjectId, GroupEnum group)
    {
      var includes = new string[]
      {
        nameof(ExamConfiguration.Class),
        nameof(ExamConfiguration.Subject)
      };
      return _repository.Table<ExamConfiguration>(includes)
        .FirstOrDefault(x => x.Subject.Id == subjectId && x.Class.Id == classId);
    }

    private bool DoesOverlap(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2)
    {
      return _timestampOverlappingChecker.DoesOverlap(startDate1, endDate1, startDate2, endDate2);
    }
  }
}