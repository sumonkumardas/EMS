using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.WorkingShiftMessages;

namespace SinePulse.EMS.Messages.TrialBalanceMessages
{
  public class ShowTrialBalanceRequestMessage : IRequest<ValidatedData<ShowTrialBalanceResponseMessage>>
  {
    public long BranchMediumId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    
    public string CurrentUserName { get; set; }
  }
}