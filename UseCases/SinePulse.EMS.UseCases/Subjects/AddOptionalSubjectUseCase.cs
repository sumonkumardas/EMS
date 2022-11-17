using System;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SubjectMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class AddOptionalSubjectUseCase : IAddOptionalSubjectUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddOptionalSubjectUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void AddOptionalSubject(AddOptionalSubjectRequestMessage requestMessage)
    {
      var student =
        _repository.GetByIdWithInclude<Student>(requestMessage.StudentId,
          new[] {nameof(Session), nameof(BranchMedium)});

      var bufferedToday = DateTime.Today - TimeSpan.FromDays(student.BranchMedium.SessionBufferPeriods);

      var currentSession = _repository.Table<Session>()
        .FirstOrDefault(s => s.BranchMedium.Id == student.BranchMedium.Id
                             && s.Status == StatusEnum.Active
                             && !s.IsSessionClosed
                             && s.StartDate <= DateTime.Today && bufferedToday <= s.EndDate);

      foreach (var subjectId in requestMessage.OptionalSubjectIds)
      {
        var classSubject = _repository.Table<ClassSubject>()
          .FirstOrDefault(cs => cs.Class.Id == requestMessage.ClassId &&
                                cs.Subject.Id == subjectId &&
                                cs.Group == requestMessage.Group);

        var subjectChoice = new SubjectChoice
        {
          Status = requestMessage.Status,
          Student = student,
          Session = currentSession,
          Subject = classSubject,
          AuditFields = new AuditFields
          {
            InsertedDateTime = DateTime.Now,
            InsertedBy = requestMessage.CurrentUserName,
            LastUpdatedBy = requestMessage.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          }
        };
        _repository.Insert(subjectChoice);
      }

      _dbContext.SaveChanges();
    }
  }
}