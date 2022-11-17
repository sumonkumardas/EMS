using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.TermTestMessages
{
  public class AddTermTestRequestMessage : IRequest<AddTermTestResponseMessage>
  {
    public decimal FullMarks { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public ExamTypeEnum ExamType { get; set; }
    public long TermId { get; set; }
    public long ClassId { get; set; }
    public long GroupId { get; set; }
    public long SubjectId { get; set; }
    public string CurrentUserName { get; set; }
  }
}