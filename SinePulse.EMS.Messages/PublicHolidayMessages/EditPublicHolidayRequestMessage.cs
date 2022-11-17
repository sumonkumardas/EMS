using System;
using MediatR;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Messages.PublicHolidayMessages
{
  public class EditPublicHolidayRequestMessage : IRequest<EditPublicHolidayResponseMessage>
  {
    public long Id { get; set; }
    public string HolidayName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
  }
}