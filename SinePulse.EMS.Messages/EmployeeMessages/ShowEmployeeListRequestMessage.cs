using MediatR;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class ShowEmployeeListRequestMessage : IRequest<ShowEmployeeListResponseMessage>
  {
    public AssociationType AssociationType { get; set; }
    public long ObjectId { get; set; }
  }
}