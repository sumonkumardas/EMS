using MediatR;

namespace SinePulse.EMS.Messages.ExamTermMessages
{
  public class GetExamTermsRequestMessage : IRequest<GetExamTermsResponseMessage>
  {
    public long SessionId { get; set; }
    public long BranchMediumId { get; set; }
  }
}