using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Holidays;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.PublicHolidayMessages
{
  public class ShowPublicHolidayListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<PublicHolidayMessageModel> PublicHolidayList { get; set; }

    public ShowPublicHolidayListResponseMessage(ValidationResult validationResult, List<PublicHolidayMessageModel> publicHolidayList)
    {
      ValidationResult = validationResult;
      PublicHolidayList = publicHolidayList;
    }
  }
}