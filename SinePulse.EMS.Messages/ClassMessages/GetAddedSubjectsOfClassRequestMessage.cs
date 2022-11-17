using MediatR;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class GetAddedSubjectsOfClassRequestMessage : IRequest<GetAddedSubjectsOfClassResponseMessage>
  {
    public long ClassId { get; set; }
  }
}