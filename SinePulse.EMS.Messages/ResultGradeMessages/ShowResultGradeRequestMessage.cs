using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ResultGradeMessages
{
  public class ShowResultGradeRequestMessage : IRequest<ValidatedData<ShowResultGradeResponseMessage>>
  {
    public long ResultGradeId { get; set; }
  }
}