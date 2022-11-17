using System.ComponentModel.DataAnnotations;

namespace SinePulse.EMS.Messages.Model.Enums
{
  public enum BloodGroupEnum
  {
    [Display(Name = "O+")] OPositive = 1,
    [Display(Name = "A+")] APositive = 2,
    [Display(Name = "B+")] BPositive = 3,
    [Display(Name = "AB+")] ABPositive = 4,
    [Display(Name = "AB-")] ABNegative = 5,
    [Display(Name = "A-")] ANegative = 6,
    [Display(Name = "B-")] BNegative = 7,
    [Display(Name = "O-")] ONegative = 8
  }
}