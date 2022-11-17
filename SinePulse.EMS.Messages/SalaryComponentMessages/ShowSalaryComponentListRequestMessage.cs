using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.SalaryComponentMessages
{
  public class ShowSalaryComponentListRequestMessage : IRequest<ValidatedData<ShowSalaryComponentListResponseMessage>>
  {
    
  }
}
