using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sections
{
  public class AddSectionUseCase : IAddSectionUseCase
  {
    private readonly IRepository _repository;

    public AddSectionUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public Section AddSection(AddSectionRequestMessage request)
    {
      var session = _repository.GetById<Session>(request.SessionId);
      var @class = _repository.GetById<Class>(request.ClassId);
      var branchMedium = _repository.GetById<BranchMedium>(request.BranchMediumId);

      var section = new Section
      {
        SectionName = request.SectionName,
        DurationOfClass = request.DurationOfClass,
        NumberOfStudents = request.NumberOfStudents,
        Group = request.Group,
        TotalClasses = request.TotalClasses,
        Status = StatusEnum.Active,
        BranchMedium = branchMedium,
        Class = @class,
        Session = session,

        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _repository.Insert(section);
      return section;
    }
  }
}