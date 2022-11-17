using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BranchMessages
{
  public class EditBranchRequestMessage : IRequest<EditBranchResponseMessage>
  {
    public long Id { get; set; }
    public string BranchName { get; set; }
    public string BranchCode { get; set; }
    public string MobileNo { get; set; }
    public string Email { get; set; }
    public string Fax { get; set; }
    public string MapIFrame { get; set; }
    public StatusEnum Status { get; set; }
    public long InstituteId { get; set; }
    public string CurrentUserName { get; set; }
  }
}