using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ExamConfigurationMessages
{
  public class DisplayAddExamConfigurationPageRequestMessage : IRequest<ValidatedData<DisplayAddExamConfigurationPageResponseMessage>>
  {
    public long? ClassId { get; set; }
    public long? GroupId { get; set; }
    public long? SubjectId { get; set; }
  }
}