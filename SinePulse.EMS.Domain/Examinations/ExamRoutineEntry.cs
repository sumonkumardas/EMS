using System;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Examinations
{
  public class ExamRoutineEntry : BaseEntity
  {
    public virtual ClassTest Test { get; set; }
    public virtual DateTime TestTime { get; set; }
  }
}