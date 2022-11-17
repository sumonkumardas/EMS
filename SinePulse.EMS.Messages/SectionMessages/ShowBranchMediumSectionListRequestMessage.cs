using MediatR;

namespace SinePulse.EMS.Messages.SectionMessages
{
  public class ShowBranchMediumSectionListRequestMessage : IRequest<ShowBranchMediumSectionListResponseMessage>
  {
    public long BranchId { get; set; }
    public long MediumId { get; set; }
  }
}