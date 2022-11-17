using MediatR;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.ClassRoutineMessages
{
  public class ShowClassRoutineRequestMessage : IRequest<ShowClassRoutineResponseMessage>
  {
    public long ClassRoutineId { get; set; }
  }
}