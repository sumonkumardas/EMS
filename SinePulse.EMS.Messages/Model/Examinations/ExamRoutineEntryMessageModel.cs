using System;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Examinations
{
  public class ExamRoutineEntryMessageModel : BaseEntityMessageModel
  {
    public  TestMessageModel Test { get; set; }
    public  DateTime TestTime { get; set; }
  }
}