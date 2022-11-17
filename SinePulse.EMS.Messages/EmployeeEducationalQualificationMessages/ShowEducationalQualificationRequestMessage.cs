using MediatR;

namespace SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages
{
  public class ShowEducationalQualificationRequestMessage : IRequest<ShowEducationalQualificationResponseMessage>
  {
    public long EducationalQualificationId { get; set; } 
  }
}