using MediatR;

namespace SinePulse.EMS.Messages.SalarySheetMessages
{
  public class GetBankStatementRequestMessage : IRequest<GetBankStatementResponseMessage>
  {
    public int Year { get; set; }
    public int Month { get; set; }
    public long BranchMediumId { get; set; }
  }
}