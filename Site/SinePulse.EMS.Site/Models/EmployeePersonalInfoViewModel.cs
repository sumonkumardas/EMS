using System;

namespace SinePulse.EMS.Site.Models
{
  public class EmployeePersonalInfoViewModel : BaseViewModel
  {
    public DateTime? DOB;
    public bool IsAccountRegistered;
    public string NationalId { get; set; }
    public string MobileNo { get; set; }
    public string EmailAddress { get; set; }
    public DateTime? JoiningDate { get; set; }
    public string BankAccountNo { get; set; }
    public string EmployeeType { get; set; }
    public string ReportTo { get; set; }
    public long ReportToEmployeeId { get; set; }
    public string FathersName { get; set; }
    public string MothersName { get; set; }
    public string SpouseName { get; set; }
    public string Gender { get; set; }
    public string Religion { get; set; }
    public string BloodGroup { get; set; }
  }
}