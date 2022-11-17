using System;
using SinePulse.EMS.Domain.Academic;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class ShowSectionViewModel : BaseViewModel
  {
    public ShowSectionViewModel()
    {
      Section = new Section();
      NumberToStringDictonary = new Dictionary<int, string>
      {
        {1, "1st"},
        {2, "2nd"},
        {3, "3rd"},
        {4, "4th"},
        {5, "5th"},
        {6, "6th"},
        {7, "7th"},
        {8, "8th"},
        {9, "9th"},
        {10, "10th"},
        {11, "11th"},
        {12, "12th"}
      };

      WeekDayDictonary = new Dictionary<int, string>
      {
        {(int) DayOfWeek.Sunday, "Sunday"},
        {(int) DayOfWeek.Monday, "Monday"},
        {(int) DayOfWeek.Tuesday, "Tuesday"},
        {(int) DayOfWeek.Wednesday, "Wednesday"},
        {(int) DayOfWeek.Thursday, "Thursday"},
        {(int) DayOfWeek.Friday, "Friday"},
        {(int) DayOfWeek.Saturday, "Saturday"}
      };
    }
    public Section Section { get; set; }
    public BreakTime BreakTime { get; set; }
    public Dictionary<int, string> NumberToStringDictonary { get; set; }
    public Dictionary<int, string> WeekDayDictonary { get; set; }
    public List<ClassTestViewModel> ClassTests { get; set; }
    public int BreakTimePosition { get; set; }
    public List<Routine> Routines { get; set; }

    public class Routine
    {
      public long RoutineId { get; set; }
      public string WeekDayName { get; set; }
      public long SubjectId { get; set; }
      public string SubjectName { get; set; }
      public long TeacherId { get; set; }
      public string TeacherName { get; set; }
      public string StartTime { get; set; }
      public string EndTime { get; set; }
    }
  }
}
