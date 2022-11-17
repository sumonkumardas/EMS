using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.ProjectPrimitives;
using System;

namespace SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages
{
  public class AddEmployeeEducationalQualificationRequestMessage : MediatR.IRequest<AddEmployeeEducationalQualificationResponseMessage>
  {
    public string InstituteName { get; set; }
    public EducationDegreeEnum DegreeName { get; set; }
    public string MajorSubject { get; set; }
    public string PassingYear { get; set; }
    public long EmployeeId { get; set; }
    public string CurrentUserName { get; set; }
  }
}