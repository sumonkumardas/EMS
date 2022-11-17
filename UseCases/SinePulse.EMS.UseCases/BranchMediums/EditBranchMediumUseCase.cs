using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class EditBranchMediumUseCase : IEditBranchMediumUseCase
  {
    private readonly IRepository _repository;

    public EditBranchMediumUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditBranchMedium(EditBranchMediumRequestMessage request)
    {
      var branchMedium = _repository.GetById<BranchMedium>(request.BranchMediumId);
      var branch = _repository.GetById<Branch>(request.BranchId);
      var medium = _repository.GetById<Medium>(request.MediumId);
      var shift = _repository.GetById<Shift>(request.ShiftId);
      branchMedium.WeeklyHolidays = ToWeeklyHolidays(request.WeeklyHolidaysList);
      branchMedium.Branch = branch;
      branchMedium.Medium = medium;
      branchMedium.Shift = shift;
      branchMedium.SessionBufferPeriods = request.SessionBufferPeriod;
      branchMedium.AuditFields.LastUpdatedBy = request.CurrentUserName;
      branchMedium.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
    
    private WeekDays ToWeeklyHolidays(IEnumerable<WeekDays> weeklyHolidaysList)
    {
      var days = weeklyHolidaysList.Sum(weekDays => (int) weekDays);
      return (WeekDays)days;
    }
  }
}