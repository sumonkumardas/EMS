using MediatR;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.BranchMessages
{
  public class ShowBranchRequestMessage : IRequest<ShowBranchResponseMessage>
  {
    public long BranchId { get; set; }
    public ICollection<Session> InstituteSession { get; set; }
  }
}