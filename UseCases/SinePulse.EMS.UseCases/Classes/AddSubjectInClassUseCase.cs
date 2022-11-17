using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Classes
{
  public class AddSubjectInClassUseCase : IAddSubjectInClassUseCase
  {
    private readonly IRepository _repository;

    public AddSubjectInClassUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void AddSubjectInClass(AddSubjectInClassRequestMessage requestMessage)
    {
      var @class = _repository.GetById<Class>(requestMessage.ClassId);
      foreach (var subjectId in requestMessage.SubjectIds)
      {
        var subject = _repository.GetById<Subject>(Convert.ToInt64(subjectId));
        var groupSubject = new ClassSubject
        {
          Subject = subject,
          Class = @class,
          Group = requestMessage.Group,
          IsOptional = requestMessage.IsOptional,
          AuditFields = new AuditFields
          {
            InsertedBy = requestMessage.CurrentUserName,
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = requestMessage.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          }
        };
        _repository.Insert(groupSubject);
      }
    }
  }
}