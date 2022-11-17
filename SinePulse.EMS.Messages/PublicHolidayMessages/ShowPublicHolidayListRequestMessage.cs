using MediatR;

namespace SinePulse.EMS.Messages.PublicHolidayMessages
{
  public class ShowPublicHolidayListRequestMessage : IRequest<ShowPublicHolidayListResponseMessage>
  {
    public int? Year { get; set; }
  }
}