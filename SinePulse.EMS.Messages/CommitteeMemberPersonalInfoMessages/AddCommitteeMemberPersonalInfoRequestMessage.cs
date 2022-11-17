using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages
{
  public class AddCommitteeMemberPersonalInfoRequestMessage : MediatR.IRequest<AddCommitteeMemberPersonalInfoResponseMessage>
  {
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string SpouseName { get; set; }
    public GenderEnum Gender { get; set; }
    public ReligionEnum Religion { get; set; }
    public BloodGroupEnum BloodGroup { get; set; }
    public long CommitteeMemberId { get; set; }

    public string CurrentUserName { get; set; }
  }
}
