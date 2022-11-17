using SinePulse.EMS.Domain.Holidays;
using SinePulse.EMS.Messages.PublicHolidayMessages;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public interface IEditPublicHolidayUseCase
  {
    void EditPublicHoliday(EditPublicHolidayRequestMessage request);
  }
}