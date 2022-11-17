using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sections
{
  public class EditSectionUseCase : IEditSectionUseCase
  {
    private readonly IRepository _repository;

    public EditSectionUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditSection(EditSectionRequestMessage request)
    {
      var session = _repository.GetById<Session>(request.SessionId);
      var @class = _repository.GetById<Class>(request.ClassId);
      var branchMedium = _repository.GetById<BranchMedium>(request.BranchMediumId);
      var section = _repository.GetById<Section>(request.Id);

      section.SectionName = request.SectionName;
      section.DurationOfClass = request.DurationOfClass;
      section.NumberOfStudents = request.NumberOfStudents;
      section.Group = request.Group;
      section.TotalClasses = request.TotalClasses;
      section.Status = request.Status;
      section.BranchMedium = branchMedium;
      section.Class = @class;
      section.Session = session;
      section.Status = Domain.Enums.StatusEnum.Active;
      section.AuditFields.LastUpdatedBy = request.CurrentUserName;
      section.AuditFields.LastUpdatedDateTime = DateTime.Now;
      
    }
  }
}