using MediatR;

namespace SinePulse.EMS.Messages.StudentSectionMessages
{
  public class ShowStudentSectionListRequestMessage : IRequest<ShowStudentSectionListResponseMessage>
  {
    public long ClassId { get; set; }
    public long SectionId { get; set; }
  }
}