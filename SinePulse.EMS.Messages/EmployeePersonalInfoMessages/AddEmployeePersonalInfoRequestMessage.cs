using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.ProjectPrimitives;
using System;

namespace SinePulse.EMS.Messages.EmployeePersonalInfoMessages
{
  public class AddEmployeePersonalInfoRequestMessage : MediatR.IRequest<AddEmployeePersonalInfoResponseMessage>
  {
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string SpouseName { get; set; }
    public GenderEnum Gender { get; set; }
    public ReligionEnum Religion { get; set; }
    public BloodGroupEnum BloodGroup { get; set; }
    public long EmployeeId { get; set; }

    public string CurrentUserName { get; set; }
  }
}