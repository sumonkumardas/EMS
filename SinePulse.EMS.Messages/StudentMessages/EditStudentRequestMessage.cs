using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;
using RelationTypeEnum = SinePulse.EMS.Messages.Model.Enums.RelationTypeEnum;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class EditStudentRequestMessage : IRequest<EditStudentResponseMessage>
  {
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public string CurrentUserName { get; set; }
    public StatusEnum Status { get; set; }
    public long StudentId { get; set; }
    public int RollNo { get; set; }
    public long ClassId { get; set; }
    public GroupEnum Group { get; set; }
    public long SectionId { get; set; }
    public RelationTypeEnum Guardian { get; set; }
  }
}