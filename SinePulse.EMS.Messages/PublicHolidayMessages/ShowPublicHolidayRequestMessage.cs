using MediatR;

namespace SinePulse.EMS.Messages.PublicHolidayMessages
{
  public class ShowPublicHolidayRequestMessage : IRequest<ShowPublicHolidayResponseMessage>
  {
    public long PublicHolidayId { get; set; }
  }
}