using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.SalaryComponentTypeMessage
{
  public class AddSalaryComponentTypeRequestMessage : IRequest<AddSalaryComponentTypeResponseMessage>
  {
    public string ComponentType { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
  }
}
