
using SinePulse.EMS.Messages.Model.Holidays;
using SinePulse.EMS.Messages.PublicHolidayMessages;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public interface IShowPublicHolidayUseCase
  {
    PublicHolidayMessageModel ShowPublicHoliday(ShowPublicHolidayRequestMessage request);
  }
}