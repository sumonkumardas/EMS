using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ExamConfigurationMessages
{
  public class ShowExamConfigurationRequestMessage : IRequest<ValidatedData<ShowExamConfigurationResponseMessage>>
  {
    public long ExamConfigurationId { get; set; }
  }
}