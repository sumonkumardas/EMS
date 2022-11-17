using System;
using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.BalanceSheetMessages
{
  public class ShowBalanceSheetRequestMessage : IRequest<ValidatedData<ShowBalanceSheetResponseMessage>>
  {
    public long BranchMediumId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
  }
}