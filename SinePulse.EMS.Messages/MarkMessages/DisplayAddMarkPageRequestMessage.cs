using MediatR;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class DisplayAddMarkPageRequestMessage : IRequest<ValidatedData<DisplayAddMarkPageResponseMessage>>
  {
    public long TestId { get; set; }
    public long? SessionId { get; set; }
    public long? TermId { get; set; }
    public long? SubjectId { get; set; }
    public ExamTypeEnum? ExamType { get; set; }
    public long SectionId { get; set; }
  }
} 