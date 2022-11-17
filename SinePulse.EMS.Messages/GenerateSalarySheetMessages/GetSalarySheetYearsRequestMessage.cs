using MediatR;

namespace SinePulse.EMS.Messages.GenerateSalarySheetMessages
{
  public class GetSalarySheetYearsRequestMessage : IRequest<GetSalarySheetYearsResponseMessage>
  {
    public long BranchMediumId { get; set; }
  }
}