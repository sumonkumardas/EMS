using MediatR;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class ShowStudentListRequestMessage : IRequest<ShowStudentListResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}