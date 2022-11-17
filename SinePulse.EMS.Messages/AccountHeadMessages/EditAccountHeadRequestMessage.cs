using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.AccountHeadMessages
{
  public class EditAccountHeadRequestMessage : IRequest<EditAccountHeadResponseMessage>
  {
    public string AccountCode { get; set; }
    public string AccountHeadName { get; set; }
    public AccountHeadTypeEnum AccountHeadType { get; set; }
    public AccountPeriodEnum AccountPeriod { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
    public long ParentHeadId { get; set; }
    public long AccountHeadId { get; set; }
  }
}