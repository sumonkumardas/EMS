using MediatR;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SalaryComponentTypeMessage
{
  public class EditSalaryComponentTypeRequestMessage : IRequest<ValidatedData<EditSalaryComponentTypeResponseMessage>>
  {
    public long SalaryComponentTypeId { get; set; }
    public string ComponentType { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
  }
}
