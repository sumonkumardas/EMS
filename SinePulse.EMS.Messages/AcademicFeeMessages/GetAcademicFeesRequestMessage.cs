using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.AcademicFeeMessages
{
  public class GetAcademicFeesRequestMessage : IRequest<GetAcademicFeesResponseMessage>
  {
    public AcademicFeePeriodEnum FeePeriod { get; set; }
    public long ClassId { get; set; }
    public long SessionId { get; set; }
    public long BranchMediumId { get; set; }
  }
}
