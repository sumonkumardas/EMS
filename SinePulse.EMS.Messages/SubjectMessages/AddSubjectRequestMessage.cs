using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class AddSubjectRequestMessage : IRequest<AddSubjectResponseMessage>
  {
    public string SubjectName { get; set; }
    public int SubjectCode { get; set; }
    public string CurrentUserName { get; set; }
  }
}