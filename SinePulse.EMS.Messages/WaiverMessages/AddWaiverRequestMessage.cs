using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.WaiverMessages
{
  public class AddWaiverRequestMessage : IRequest<AddWaiverResponseMessage>
  {
    public long StudentId { get; set; }
    public long ClassId { get; set; }
    public long SectionId { get; set; }
    public decimal[] Waivers { get; set; }
    public IEnumerable<AcademicFeeMessageModel> AcademicFees { get; set; }
    public int[] InPercentages { get; set; }
    public string CurrentUserName { get; set; }
    public long SessionId { get; set; }
    public long BranchMediumId { get; set; }
    public bool[] InPercentagesBooleanArray { get; set; }
  }
}