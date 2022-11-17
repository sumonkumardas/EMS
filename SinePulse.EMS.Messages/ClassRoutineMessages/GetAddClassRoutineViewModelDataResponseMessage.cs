using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Examinations;

namespace SinePulse.EMS.Messages.ClassRoutineMessages
{
  public class GetAddClassRoutineViewModelDataResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<RoomMessageModel> Rooms { get; }
    public IEnumerable<EmployeeMessageModel> Teachers { get; }
    public IEnumerable<SubjectMessageModel> Subjects { get; }

    public GetAddClassRoutineViewModelDataResponseMessage(ValidationResult validationResult,
      IEnumerable<RoomMessageModel> rooms, IEnumerable<EmployeeMessageModel> teachers,
      IEnumerable<SubjectMessageModel> subjects)
    {
      ValidationResult = validationResult;
      Rooms = rooms;
      Teachers = teachers;
      Subjects = subjects;
    }
  }
}