using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.SectionMessages
{
  public class AddSectionRequestMessage : IRequest<AddSectionResponseMessage>
  {
    public string SectionName { get; set; }
    public int NumberOfStudents { get; set; }
    public int TotalClasses { get; set; }
    public int DurationOfClass { get; set; }
    public StatusEnum Status { get; set; }
    public long ClassId { get; set; }
    public long BranchMediumId { get; set; }
    public long SessionId { get; set; }
    public GroupEnum Group { get; set; }
    public string CurrentUserName { get; set; }
  }
}