using System;
using FluentValidation;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class ShowPublicHolidayListRequestMessageValidator : AbstractValidator<ShowPublicHolidayListRequestMessage>
  {

    public ShowPublicHolidayListRequestMessageValidator()
    {
    }
    
  }
}