using MediatR;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class ShowEmployeeRequestMessage : IRequest<ShowEmployeeResponseMessage>
  {
    public long EmployeeId { get; set; }
  }
}