using System;
using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Messages.BreakTimeMessages
{
  public class AddBreakTimeRequestMessage : IRequest<AddBreakTimeResponseMessage>
  {
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public StatusEnum Status { get; set; } 
    public string CurrentUserName { get; set; }
    public long BranchMediumId { get; set; }
  }
}