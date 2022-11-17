using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Holidays;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class EditPublicHolidayUseCase : IEditPublicHolidayUseCase
  {
    private readonly IRepository _repository;

    public EditPublicHolidayUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditPublicHoliday(EditPublicHolidayRequestMessage request)
    {
      var holiday = _repository.GetById<PublicHoliday>(request.Id);

      holiday.HolidayName = request.HolidayName;
      holiday.StartDate = request.StartDate;
      holiday.EndDate = request.EndDate;
      holiday.Status = (Domain.Enums.StatusEnum)request.Status;
      holiday.AuditFields.LastUpdatedBy = request.CurrentUserName;
      holiday.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
  }
}