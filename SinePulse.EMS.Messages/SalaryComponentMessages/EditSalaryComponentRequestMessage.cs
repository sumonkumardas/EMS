using MediatR;

namespace SinePulse.EMS.Messages.SalaryComponentMessages
{
  public class EditSalaryComponentRequestMessage : IRequest<EditSalaryComponentResponseMessage>
  {
    public long ComponentId { get; set; }
    public string ComponentName { get; set; }
    public long ComponentTypeId { get; set; }
    public string CurrentUserName { get; set; }
  }
}
