using MediatR;

namespace SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages
{
  public class GetEmployeeEducationalQualificationsRequestMessage : IRequest<GetEmployeeEducationalQualificationsResponseMessage>
  {
    public long EmployeeId { get; set; }
  }
}