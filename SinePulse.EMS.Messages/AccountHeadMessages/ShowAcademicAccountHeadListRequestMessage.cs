using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.AccountHeadMessages
{
  public class ShowAcademicAccountHeadListRequestMessage : IRequest<ShowAcademicAccountHeadListResponseMessage>
  {
    public AccountPeriodEnum AccountPeriod { get; set; }
  }
}