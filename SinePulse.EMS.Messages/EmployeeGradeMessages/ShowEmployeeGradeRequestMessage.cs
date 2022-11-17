using MediatR;
using SinePulse.EMS.Messages.EmployeeGradeMessages;

namespace SinePulse.EMS.Messages.EmployeeGradeMessages
{
  public class ShowEmployeeGradeRequestMessage : IRequest<ShowEmployeeGradeResponseMessage>
  {
    public long GradeId { get; set; }
  }
}