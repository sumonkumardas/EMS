using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ClassTestMessages
{
  public class ShowClassTestListRequestMessage : IRequest<ValidatedData<ShowClassTestListResponseMessage>>
  {
    public long SectionId { get; set; }
  }
}