using System;
using MediatR;

namespace SinePulse.EMS.Messages.PublicHolidayMessages
{
  public class AddPublicHolidayRequestMessage : IRequest<AddPublicHolidayResponseMessage>
  {
    public string HolidayName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string CurrentUserName { get; set; }
  }
}