using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages
{
  public class EditEducationalQualificationRequestMessage : IRequest<EditEducationalQualificationResponseMessage>
  {
    public long EducationalQualificationId { get; set; }
    public string InstituteName { get; set; }
    public EducationDegreeEnum DegreeName { get; set; }
    public string MajorSubject { get; set; }
    public string PassingYear { get; set; }
    public string CurrentUserName { get; set; }
  }
}