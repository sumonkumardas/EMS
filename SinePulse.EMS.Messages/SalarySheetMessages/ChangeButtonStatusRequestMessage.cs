using MediatR;

namespace SinePulse.EMS.Messages.SalarySheetMessages
{
  public class ChangeButtonStatusRequestMessage : IRequest<ChangeButtonStatusResponseMessage>
  {
    public int Month { get; set; }
    public int Year { get; set; }
    public long BranchMediumId { get; set; }
  }
}