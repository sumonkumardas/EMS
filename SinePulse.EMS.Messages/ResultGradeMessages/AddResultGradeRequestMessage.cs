using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ResultGradeMessages
{
  public class AddResultGradeRequestMessage : IRequest<ValidatedData<AddResultGradeResponseMessage>>
  {
    public string GradeLetter { get; set; }
    public decimal GradePoint { get; set; }
    public int StartMark { get; set; }
    public int EndMark { get; set; }
    public long BranchMediumId { get; set; }
    public string CurrentUserName { get; set; }
  }
}