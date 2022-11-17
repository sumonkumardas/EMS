using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.WaiverMessages
{
  public class GetStudentWaiversRequestMessage : IRequest<GetStudentWaiversResponseMessage>
  {
    public long StudentId { get; set; }
    public long SessionId { get; set; }
    public long SectionId { get; set; }
    public AcademicFeePeriodEnum FeePeriod { get; set; }
  }
}