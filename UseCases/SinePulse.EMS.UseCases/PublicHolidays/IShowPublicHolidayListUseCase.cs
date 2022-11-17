
using SinePulse.EMS.Messages.Model.Holidays;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using System.Collections.Generic;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public interface IShowPublicHolidayListUseCase
  {
    List<PublicHolidayMessageModel> ShowPublicHolidayList(ShowPublicHolidayListRequestMessage request);
  }
}