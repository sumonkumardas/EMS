using System;
using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.IncomeStatementMessages
{
  public class ShowIncomeStatementRequestMessage : IRequest<ValidatedData<ShowIncomeStatementResponseMessage>>
  {
    public long BranchMediumId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    
    public string CurrentUserName { get; set; }
  }
}