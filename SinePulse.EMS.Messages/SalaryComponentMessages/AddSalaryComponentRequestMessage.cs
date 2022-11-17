using MediatR;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Payroll;

namespace SinePulse.EMS.Messages.SalaryComponentMessages
{
  public class AddSalaryComponentRequestMessage : IRequest<AddSalaryComponentResponseMessage>
  {
    public string ComponentName { get; set; }
    public long ComponentTypeId { get; set; }
    public string CurrentUserName { get; set; }
  }
}
