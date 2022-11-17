using MediatR;

namespace SinePulse.EMS.Messages.SalarySheetMessages
{
  public class GetSalarySheetRequestMessage : IRequest<GetSalarySheetResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
  }
}