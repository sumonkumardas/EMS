using MediatR;
using SinePulse.EMS.Messages.ClassRoutineMessages;

namespace SinePulse.EMS.Messages.ClassRoutineMessages
{
  public class ShowClassRoutineListRequestMessage : IRequest<ShowClassRoutineListResponseMessage>
  {
    public long SectionId { get; set; }
  }
}