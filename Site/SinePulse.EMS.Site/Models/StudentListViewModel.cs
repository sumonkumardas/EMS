using System;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class StudentListViewModel : BaseViewModel
  {
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string NationalIdCardNo { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public StatusEnum Status { get; set; }
  }
}