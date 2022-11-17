using SinePulse.EMS.Domain.Holidays;
using SinePulse.EMS.Messages.PublicHolidayMessages;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public interface IAddPublicHolidayUseCase
  {
    PublicHoliday AddPublicHoliday(AddPublicHolidayRequestMessage request);
  }
}