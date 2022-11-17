using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SinePulse.EMS.NO.Domain.Enums
{
  public enum BloodGroupEnum
  {
    [Display(Name = "O+")]
    OPositive,
    [Display(Name = "A+")]
    APositive,
    [Display(Name = "B+")]
    BPositive,
    [Display(Name = "AB+")]
    ABPositive,
    [Display(Name = "AB-")]
    ABNegative,
    [Display(Name = "A-")]
    ANegative,
    [Display(Name = "B-")]
    BNegative,
    [Display(Name = "O-")]
    ONegative
  }
}
