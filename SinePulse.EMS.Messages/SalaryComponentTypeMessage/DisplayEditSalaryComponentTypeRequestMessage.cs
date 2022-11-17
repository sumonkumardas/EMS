using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SalaryComponentTypeMessage
{
  public class DisplayEditSalaryComponentTypeRequestMessage : IRequest<ValidatedData<DisplayEditSalaryComponentTypeResponseMessage>>
  {
    public long SalaryComponentTypeId { get; set; }
  }
}
