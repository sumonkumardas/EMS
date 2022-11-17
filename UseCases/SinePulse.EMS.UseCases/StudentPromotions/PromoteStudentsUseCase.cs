using System;
using System.Collections.Immutable;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentPromotionMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public class PromoteStudentsUseCase : IPromoteStudentsUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public PromoteStudentsUseCase(IRepository repository, EmsDbContext dbContext,
      ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _dbContext = dbContext;
      _currentSessionProvider = currentSessionProvider;
    }

    public PromoteStudentsResponseMessage PromoteStudents(PromoteStudentsRequestMessage requestMessage)
    {
      var currentStudentSectionIds = requestMessage.StudentPromotionRequests.Select(x => x.CurrentStudentSectionId)
        .ToImmutableHashSet();

      var currentSection = _repository.GetById<Section>(requestMessage.CurrentSectionId);
      var currentSession = _currentSessionProvider.GetCurrentSession(currentSection.BranchMediumId);
      var lastTerm = _repository.Filter<ExamTerm>(x => x.Session.Id == currentSession.Id).OrderBy(x => x.StartDate)
        .Last();

      var currentStudentSectionIdResultSheetMap = _repository.Filter<ResultSheet>(x =>
          x.TermId == lastTerm.Id && currentStudentSectionIds.Contains(x.StudentSectionId))
        .GroupBy(x => x.StudentSectionId)
        .ToDictionary(x => x.Key, x => x.Single());

      var nextSectionIdPromotionInfosMap = requestMessage.StudentPromotionRequests
        .GroupBy(x => x.NextSectionId)
        .ToDictionary(x => x.Key, x => x.Select(y =>
          new
          {
            y.StudentId,
            Result = new
            {
              currentStudentSectionIdResultSheetMap[y.CurrentStudentSectionId].Gpa,
              currentStudentSectionIdResultSheetMap[y.CurrentStudentSectionId].TotalMark
            }
          }).ToList());
      var nextSectionIds = nextSectionIdPromotionInfosMap.Select(x => x.Key).ToImmutableHashSet();
      var nextSectionIdSectionMap = _repository.Filter<Section>(
          x => nextSectionIds.Contains(x.Id))
        .GroupBy(x => x.Id)
        .ToDictionary(x => x.Key, x => x.Single());
      var sectionIdAlreadyAssignedRollNosMap = _repository.Filter<StudentSection>(
          x => nextSectionIds.Contains(x.Section.Id))
        .GroupBy(x => x.Id)
        .ToDictionary(x => x.Key, x => x.Select(y => y.RollNo).ToList());


      foreach (var nextSectionId in nextSectionIds)
      {
        var nextSection = nextSectionIdSectionMap[nextSectionId];
        var promotionInfos = nextSectionIdPromotionInfosMap[nextSectionId]
          .OrderByDescending(x => x.Result.TotalMark)
          .ThenByDescending(x => x.Result.Gpa);
        var nextRollNo = 1;
        foreach (var promotionInfo in promotionInfos)
        {
          while (sectionIdAlreadyAssignedRollNosMap[nextSectionId].Contains(nextRollNo))
          {
            nextRollNo++;
          }

          var newStudentSection = new StudentSection
          {
            RollNo = nextRollNo,
            Group = nextSection.Group,
            StudentId = promotionInfo.StudentId,
            Section = nextSection,
            ClassId = nextSection.ClassId,
            AuditFields = new AuditFields
            {
              InsertedBy = requestMessage.CurrentUserName,
              InsertedDateTime = DateTime.Now,
              LastUpdatedBy = requestMessage.CurrentUserName,
              LastUpdatedDateTime = DateTime.Now
            }
          };

          _repository.Insert(newStudentSection);
        }
      }

      _dbContext.SaveChanges();

      return new PromoteStudentsResponseMessage();
    }
  }
  
}


namespace SinePulse.EMS.UseCases.StudentPromotions
{
}