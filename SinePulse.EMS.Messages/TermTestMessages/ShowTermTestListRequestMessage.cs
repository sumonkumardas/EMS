using MediatR;

namespace SinePulse.EMS.Messages.TermTestMessages
{
  public class ShowTermTestListRequestMessage : IRequest<ShowTermTestListResponseMessage>
  {
    public long TermId { get; set; }
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
  }
}