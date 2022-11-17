using MediatR;

namespace SinePulse.EMS.Messages.SalarySheetMessages
{
  public class SalarySheetAccountPostingRequestMessage: IRequest<SalarySheetAccountPostingResponseMessage>
  {
    public int Month { get; set; }
    public int Year { get; set; }
    public long BranchMediumId { get; set; }
    public string CurrentUserName { get; set; }
  }
}