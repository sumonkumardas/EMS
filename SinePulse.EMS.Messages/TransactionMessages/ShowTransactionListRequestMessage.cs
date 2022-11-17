using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.TransactionMessages
{
  public class ShowTransactionListRequestMessage : IRequest<ShowTransactionListResponseMessage>
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public long BranchMediumId { get; set; }
  }
}