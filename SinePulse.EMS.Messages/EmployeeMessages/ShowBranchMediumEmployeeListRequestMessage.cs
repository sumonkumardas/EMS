using MediatR;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class ShowBranchMediumEmployeeListRequestMessage : IRequest<ShowEmployeeListResponseMessage>
  {
    public AssociationType AssociationType { get; set; }
    public long BranchMediumId { get; set; }
  }
}
