using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SalaryComponentTypeMessage
{
  public class ShowSalaryComponentTypeListRequestMessage : IRequest<ValidatedData<ShowSalaryComponentTypeListResponseMessage>>
  {

  }
}
