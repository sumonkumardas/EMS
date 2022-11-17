using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SalaryComponentMessages
{
  public class DisplayEditSalaryComponentRequestMessage : IRequest<ValidatedData<DisplayEditSalaryComponentResponseMessage>>
  {
    public long SalaryComponentId { get; set; }
  }
}
