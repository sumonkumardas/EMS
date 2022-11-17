using MediatR;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class AddOptionalSubjectRequestMessage : IRequest<AddOptionalSubjectResponseMessage>
  {
    public long StudentId { get; set; }
    public long ClassId { get; set; }
    public long[] OptionalSubjectIds { get; set; }
    public string CurrentUserName { get; set; }
    public StatusEnum Status { get; set; }
    public GroupEnum Group { get; set; }
    public long SectionId { get; set; }
  }
}