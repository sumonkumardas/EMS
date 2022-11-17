using MediatR;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class GetSubjectsToAddInClassRequestMessage : IRequest<GetSubjectsToAddInClassResponseMessage>
  {
    public long ClassId { get; set; }
  }
}