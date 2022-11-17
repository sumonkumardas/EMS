using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.ClassTestMessages
{
  public class AddClassTestRequestMessage : IRequest<AddClassTestResponseMessage>
  {
    public string TestName { get; set; }
    public decimal FullMarks { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public ExamTypeEnum ExamType { get; set; }

    public long ExamConfigurationId { get; set; }
    public long SectionId { get; set; }
    public string CurrentUserName { get; set; }
  }
}