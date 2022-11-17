using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class ShowStudentRequestMessage : IRequest<ValidatedData<ShowStudentResponseMessage>>
  {
    public long StudentId { get; set; }
    public long BranchMediumId { get; set; }
  }
}