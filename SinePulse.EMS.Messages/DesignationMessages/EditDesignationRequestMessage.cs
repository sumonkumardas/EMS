using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.DesignationMessages
{
  public class EditDesignationRequestMessage : IRequest<EditDesignationResponseMessage>
  {
    public string DesignationName { get; set; }
    public StatusEnum Status { get; set; }
    public long DesignationId { get; set; }
    public string CurrentUserName { get; set; }
    public EmployeeTypeEnum EmployeeType { get; set; }
  }
}