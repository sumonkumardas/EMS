using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class ShowMarkRequestMessage : IRequest<ValidatedData<ShowMarkResponseMessage>>
  {
    public long MarkId { get; set; }
  }
}