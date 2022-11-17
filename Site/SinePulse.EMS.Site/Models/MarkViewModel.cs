using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class MarkViewModel : BaseViewModel
  {
    public long MarkId { get; set; }
    public decimal ObtainedMark { get; set; }
    public decimal GraceMark { get; set; }
    public string Comment { get; set; }
    public StatusEnum Status { get; set; }
    public Student StudentData { get; set; }

    public class Student
    {
      public long StudentSectionId { get; set; }
      public int RollNo { get; set; }
      public string StudentName { get; set; }
      public string ClassName { get; set; }
      public string ShiftName { get; set; }
      public string SectionName { get; set; }
    }
  }
}