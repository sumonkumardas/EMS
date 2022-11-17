using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.CommitteeMemberMessages
{
  public class AddCommitteeMemberRequestMessage : IRequest<AddCommitteeMemberResponseMessage>
  {
    public string FullName { get; set; }
    public DateTime? DOB { get; set; }
    public string MobileNo { get; set; }
    public string EmailAddress { get; set; }
    public string NationalId { get; set; }
    public StatusEnum Status { get; set; }
    public long InstituteId { get; set; }
    public long DesignationId { get; set; }
    public string CurrentUserName { get; set; }
  }
}