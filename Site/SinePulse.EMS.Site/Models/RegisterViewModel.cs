using System;

namespace SinePulse.EMS.Site.Models
{
  public class RegisterViewModel : BaseViewModel
  {
    public string FullName { get; set; }
    public DateTime DOB { get; set; }
    public DateTime JoiningDate { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}