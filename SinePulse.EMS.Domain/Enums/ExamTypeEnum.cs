using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SinePulse.EMS.Domain.Enums
{
  public enum ExamTypeEnum
  {
    [Display(Name = "Subjective")]
    Subjective = 1,
    [Display(Name = "Objective(MCQ)")]
    Objective = 2,
    [Display(Name = "Practical")]
    Practical = 3
  }
}
