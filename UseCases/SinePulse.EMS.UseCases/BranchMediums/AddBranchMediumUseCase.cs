using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class AddBranchMediumUseCase : IAddBranchMediumUseCase
  {
    private readonly IRepository _repository;

    public AddBranchMediumUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public BranchMedium AddBranchMedium(AddBranchMediumRequestMessage request)
    {
      var branch = _repository.GetById<Branch>(request.BranchId);
      var medium = _repository.GetById<Medium>(request.MediumId);
      var shift = _repository.GetById<Shift>(request.ShiftId);
      var branchMedium = new BranchMedium
      {
        WeeklyHolidays = ToWeeklyHolidays(request.WeeklyHolidaysList),
        Branch = branch,
        Medium = medium,
        Shift = shift,
        SessionBufferPeriods = request.SessionBufferPeriod,
        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };

      _repository.Insert(branchMedium);
      return branchMedium;
    }
    
    private WeekDays ToWeeklyHolidays(IEnumerable<WeekDays> weeklyHolidaysList)
    {
      var days = weeklyHolidaysList.Sum(weekDays => (int) weekDays);
      return (WeekDays)days;
    }
  }
}