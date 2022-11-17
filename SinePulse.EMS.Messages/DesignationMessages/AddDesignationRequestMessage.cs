using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.DesignationMessages
{
  public class AddDesignationRequestMessage : IRequest<AddDesignationResponseMessage>
  {
    public string DesignationName { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
    public EmployeeTypeEnum EmployeeType { get; set; }
  }
}