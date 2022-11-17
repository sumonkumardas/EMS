using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.StudentSectionMessages
{
  public class ShowStudentSectionRollRequestMessage : IRequest<ShowStudentSectionRollResponseMessage>
  {
    public long ClassId { get; set; }
    public long SectionId { get; set; }
    public GroupEnum Group { get; set; }
  }
}