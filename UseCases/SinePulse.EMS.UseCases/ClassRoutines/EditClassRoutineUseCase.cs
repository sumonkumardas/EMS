using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Routines;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class EditClassRoutineUseCase : IEditClassRoutineUseCase
  {
    private readonly IRepository _repository;

    public EditClassRoutineUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditClassRoutine(EditClassRoutineRequestMessage request)
    {
      var subject = _repository.GetById<Subject>(request.SubjectId);
      var teacher = _repository.GetById<Teacher>(request.TeacherId);
      var section = _repository.GetById<Section>(request.SectionId);
      var room = _repository.GetById<Room>(request.RoomId);
      var classRoutine = _repository.GetById<ClassRoutine>(request.Id);

      classRoutine.WeekDay = request.WeekDay;
      classRoutine.StartTime = request.StartTime;
      classRoutine.EndTime = request.EndTime;
      classRoutine.IsBreakTime = request.IsBreakTime;
      classRoutine.Status = StatusEnum.Active;
      classRoutine.AuditFields.LastUpdatedBy = request.CurrentUserName;
      classRoutine.AuditFields.LastUpdatedDateTime = DateTime.Now;
      classRoutine.Subject = subject;
      classRoutine.Teacher = teacher;
      classRoutine.Section = section;
      classRoutine.Room = room;
    }
  }
}