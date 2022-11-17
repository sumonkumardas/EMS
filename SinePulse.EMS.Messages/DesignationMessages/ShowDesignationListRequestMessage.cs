using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.DesignationMessages
{
  public class ShowDesignationListRequestMessage : IRequest<ShowDesignationListResponseMessage>
  {
    public EmployeeTypeEnum EmployeeType { get; set; }
  }
}