using System;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class RegisterDataCheckRequestMessage
  {
    public string FullName { get; set; }
    public DateTime DOB { get; set; }
    public DateTime JoiningDate { get; set; }
  }
}