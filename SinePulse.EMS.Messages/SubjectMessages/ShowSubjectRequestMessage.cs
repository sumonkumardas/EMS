using MediatR;
using SinePulse.EMS.Domain.Academic;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class ShowSubjectRequestMessage : IRequest<ShowSubjectResponseMessage>
  {
    public long SubjectId { get; set; }
    public Subject Subject { get; set; }
  }
}