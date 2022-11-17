using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.AcademicFeeMessages
{
  public class AddAcademicFeeRequestMessage : IRequest<AddAcademicFeeResponseMessage>
  {
    public decimal[] FeesCollection { get; set; }
    public long SessionId { get; set; }
    public long ClassId { get; set; }
    public long BranchMediumId { get; set; }
    public string CurrentUserName { get; set; }
    public AcademicFeePeriodEnum AcademicFeePeriod { get; set; }
    public IEnumerable<BranchMediumAccountsHeadMessageModel> AccountHeads { get; set; }
    public IEnumerable<AcademicFeeMessageModel> AcademicFees { get; set; }
  }
}