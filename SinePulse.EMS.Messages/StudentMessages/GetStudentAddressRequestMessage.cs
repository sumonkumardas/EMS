using MediatR;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class GetStudentAddressRequestMessage : IRequest<GetStudentAddressResponseMessage>
  {
    public long StudentId { get; set; }
  }
}