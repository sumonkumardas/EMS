using MediatR;

namespace SinePulse.EMS.Messages.StudentContactPersonMessages
{
  public class GetStudentContactPersonsRequestMessage : IRequest<GetStudentContactPersonsResponseMessage>
  {
    public long StudentId { get; set; }
  }
}