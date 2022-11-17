using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Routines;
using SinePulse.EMS.Messages.ImportSessionDataMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.ImportSessionDatas
{
  public class ImportSessionDataUseCase : IImportSessionDataUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public ImportSessionDataUseCase(IRepository repository, EmsDbContext dbContext,
      ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _dbContext = dbContext;
      _currentSessionProvider = currentSessionProvider;
    }

    public ImportSessionDataResponseMessage ImportSessionData(ImportSessionDataRequestMessage requestMessage)
    {
      var previousSession = _repository.GetById<Session>(requestMessage.PreviousSessionId);
      var currentSession = _currentSessionProvider.GetCurrentSession(previousSession.BranchMediumId);
      if (requestMessage.OnlySectionInfo || requestMessage.SectionInfoWithClassRoutine)
      {
        ImportSections(previousSession, currentSession, requestMessage.SectionInfoWithClassRoutine);
      }

      if (requestMessage.OnlyExamTerm || requestMessage.ExamTermWithExamConfiguration)
      {
        ImportTerms(previousSession, currentSession, requestMessage.ExamTermWithExamConfiguration);
      }

      _dbContext.SaveChanges();

      return new ImportSessionDataResponseMessage();
    }

    private void ImportSections(Session previousSession, Session currentSession, bool shouldImportClassRoutine)
    {
      var previousSections = _repository.Filter<Section, Class, BranchMedium, Room>(
        x => x.Session.Id == previousSession.Id,
        x => x.Class,
        x => x.BranchMedium,
        x => x.Room);
      foreach (var previousSection in previousSections)
      {
        var newSection = new Section
        {
          Group = previousSection.Group,
          SectionName = previousSection.SectionName,
          NumberOfStudents = previousSection.NumberOfStudents,
          TotalClasses = previousSection.TotalClasses,
          DurationOfClass = previousSection.DurationOfClass,
          Status = previousSection.Status,
          Class = previousSection.Class,
          BranchMedium = previousSection.BranchMedium,
          Session = currentSession,
          Room = previousSection.Room
        };
        _repository.Insert(newSection);

        if (shouldImportClassRoutine)
        {
          ImportClassRoutines(previousSection, newSection);
        }
      }
    }

    private void ImportClassRoutines(Section previousSection, Section newSection)
    {
      var previousRoutines = _repository.Filter<ClassRoutine, Subject, Domain.Employees.Employee, Room>(
        x => x.Section.Id == previousSection.Id,
        x => x.Subject,
        x => x.Teacher,
        x => x.Room);
      foreach (var previousRoutine in previousRoutines)
      {
        var newRoutine = new ClassRoutine
        {
          WeekDay = previousRoutine.WeekDay,
          StartTime = previousRoutine.StartTime,
          EndTime = previousRoutine.EndTime,
          IsBreakTime = previousRoutine.IsBreakTime,
          Status = previousRoutine.Status,
          Subject = previousRoutine.Subject,
          Teacher = previousRoutine.Teacher,
          Section = newSection,
          Room = previousRoutine.Room
        };
        _repository.Insert(newRoutine);
      }
    }

    private void ImportTerms(Session previousSession, Session currentSession, bool shouldImportExamConfiguration)
    {
      var previousTerms = _repository.Filter<ExamTerm>(x => x.Session.Id == previousSession.Id);
      foreach (var previousTerm in previousTerms)
      {
        var newTerm = new ExamTerm
        {
          TermName = previousTerm.TermName,
          StartDate = currentSession.StartDate + (previousTerm.StartDate - previousSession.StartDate),
          EndDate = currentSession.StartDate + (previousTerm.EndDate - previousSession.StartDate),
          Status = previousTerm.Status,
          Session = currentSession
        };
        _repository.Insert(newTerm);

        if (shouldImportExamConfiguration)
        {
          ImportExamConfigurations(previousTerm, newTerm);
        }
      }
    }

    private void ImportExamConfigurations(ExamTerm previousTerm, ExamTerm newTerm)
    {
      var previousExamConfigurations = _repository.Filter<ExamConfiguration, ExamTerm, Class, Subject>(
        x => x.Term.Id == previousTerm.Id,
        x => x.Term,
        x => x.Class,
        x => x.Subject);
      foreach (var previousExamConfiguration in previousExamConfigurations)
      {
        var newExamConfiguration = new ExamConfiguration
        {
          SubjectiveFullMark = previousExamConfiguration.SubjectiveFullMark,
          SubjectivePassMark = previousExamConfiguration.SubjectivePassMark,
          ObjectiveFullMark = previousExamConfiguration.ObjectiveFullMark,
          ObjectivePassMark = previousExamConfiguration.ObjectivePassMark,
          PracticalFullMark = previousExamConfiguration.PracticalFullMark,
          PracticalPassMark = previousExamConfiguration.PracticalPassMark,
          ClassTestPercentage = previousExamConfiguration.ClassTestPercentage,
          Status = previousExamConfiguration.Status,
          Term = newTerm,
          Class = previousExamConfiguration.Class,
          Subject = previousExamConfiguration.Subject
        };
        _repository.Insert(newExamConfiguration);
      }
    }
  }
}