using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Holidays;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class AddPublicHolidayUseCase : IAddPublicHolidayUseCase
  {
    private readonly IRepository _repository;

    public AddPublicHolidayUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public PublicHoliday AddPublicHoliday(AddPublicHolidayRequestMessage request)
    {
      var holiday = new PublicHoliday
      {
        HolidayName = request.HolidayName,
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };

      _repository.Insert(holiday);
      return holiday;
    }
  }
}