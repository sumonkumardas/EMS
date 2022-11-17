using MediatR;
using SinePulse.EMS.Domain.Enums;


namespace SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages
{
  public class AddBranchMediumAccountHeadRequestMessage : IRequest<AddBranchMediumAccountHeadResponseMessage>
  {
    public long CurrentSessionId { get; set; }
    public long BranchMediumId { get; set; }
    public long ParentAccountHeadId { get; set; }
    public string AccountCode { get; set; }
    public string AccountHeadName { get; set; }
    public AccountHeadTypeEnum AccountHeadType { get; set; }
    public AccountPeriodEnum AccountPeriod { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
    public bool IsLedger { get; set; }
  }
}