using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class PayBillViewModel : BaseViewModel
  {
    public List<int> Years { get; set; } = new List<int>();
    public int Year { get; set; }
    public MonthsOfYearEnum Month { get; set; }
    public decimal DueAmount { get; set; }
    public long BranchMediumId { get; set; }
    public string PerStudentCharge { get; set; }
    public string TotalActiveStudent { get; set; }
  }
}