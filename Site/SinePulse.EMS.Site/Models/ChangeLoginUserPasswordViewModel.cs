namespace SinePulse.EMS.Site.Models
{
  public class ChangeLoginUserPasswordViewModel : BaseViewModel
  {
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string RepeatPassword { get; set; }
    public string EmployeeCode { get; set; }
    public long EmployeeId { get; set; }
  }
}