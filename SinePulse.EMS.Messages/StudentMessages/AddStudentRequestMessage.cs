using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class AddStudentRequestMessage : IRequest<ValidatedData<AddStudentResponseMessage>>
  {
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public string CurrentUserName { get; set; }
    public long BranchMediumId { get; set; }
    public RelationTypeEnum Guardian { get; set; }
    public string FullName { get; set; }
    public long ClassId { get; set; }
    public GroupEnum Group { get; set; }
    public long SectionId { get; set; }
    public int RollNo { get; set; }
  }
}