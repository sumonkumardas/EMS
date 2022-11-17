using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class GetOptionalSubjectListRequestMessage : IRequest<GetOptionalSubjectListResponseMessage>
  {
    public long ClassId { get; set; }
    public GroupEnum Group { get; set; }
  }
}