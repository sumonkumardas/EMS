using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
  public class FeeCollectionViewModel : BaseViewModel
  {
    public long StudentId { get; set; }
    public long BranchMediumId { get; set; }
    public string StudentName { get; set; }
    public int Roll { get; set; }
    public string SectionName { get; set; }
    public long SectionId { get; set; }
    public string ClassName { get; set; }
    public long ClassId { get; set; }
    public IEnumerable<SessionMessageModel> Sessions { get; set; }
    public long SessionId { get; set; }
    public AcademicFeePeriodEnum FeeType { get; set; }
    public string BankAccountAccountHeadId { get; set; }
    public IEnumerable<BranchMediumAccountsHeadMessageModel> BankAccounts { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public MonthType Month { get; set; }
    public decimal[] Amounts { get; set; }
    public decimal[] Waivers { get; set; }
    public decimal TotalAmount { get; set; }
  }
}