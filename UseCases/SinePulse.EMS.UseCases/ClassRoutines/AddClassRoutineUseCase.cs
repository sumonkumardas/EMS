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
  public class AddClassRoutineUseCase : IAddClassRoutineUseCase
  {
    private readonly IRepository _repository;

    public AddClassRoutineUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ClassRoutine AddClassRoutine(AddClassRoutineRequestMessage request)
    {
      var subject = _repository.GetById<Subject>(request.SubjectId);
      var teacher = _repository.GetById<Teacher>(request.TeacherId);
      var section = _repository.GetById<Section>(request.SectionId);
      var room = _repository.GetById<Room>(request.RoomId);

      var classRoutine = new ClassRoutine
      {
        WeekDay = request.WeekDay,
        StartTime = request.StartTime,
        EndTime = request.EndTime,
        IsBreakTime = request.IsBreakTime,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },

        Subject = subject,
        Teacher = teacher,
        Section = section,
        Room = room
      };

      _repository.Insert(classRoutine);
      return classRoutine;
    }
  }
}