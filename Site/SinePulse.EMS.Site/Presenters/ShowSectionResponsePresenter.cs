using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Utility;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowSectionResponsePresenter
  {
    public ShowSectionViewModel Handle(ShowSectionResponseMessage message, ShowSectionViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.Section = message.Section;
      viewModel.BreakTime = message.BreakTime;
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.PendingAmount = userAssociationMessage.PendingAmount;
      viewModel.IsBillDueDateOver = userAssociationMessage.IsBillDueDateOver;
      //@Model.WeekDayDictonary.Remove((int)Model.Section.BranchMedium.WeeklyHolidays)
      var weeklyHolidayList = viewModel.Section.BranchMedium.WeeklyHolidays.ConvertToDaysOfWeek();
      foreach (var dayOfWeek in weeklyHolidayList)
      {
        viewModel.WeekDayDictonary.Remove((int)dayOfWeek);
      }

      viewModel.WeekDayDictonary.Remove((int)viewModel.Section.BranchMedium.WeeklyHolidays);
      var breakTimePostion = 0;

      foreach (var weekDayDictonary in viewModel.WeekDayDictonary)
      {
        if (viewModel.BreakTime != null)
        {
          var position = viewModel.Section.Routines.Count(x => x.WeekDay == (DayOfWeek)weekDayDictonary.Key && x.EndTime.Ticks <= viewModel.BreakTime.StartTime.Ticks);
          if (position > breakTimePostion)
            breakTimePostion = position;
        }
      }

      if (breakTimePostion == 0)
        if (viewModel.Section.TotalClasses >= 5)
          viewModel.BreakTimePosition = 4;
        else
          viewModel.BreakTimePosition = 0;
      else
        viewModel.BreakTimePosition = breakTimePostion;
      viewModel.Routines = new List<ShowSectionViewModel.Routine>();
      if (viewModel.BreakTime != null)
      {
        foreach (var wkdayModel in viewModel.WeekDayDictonary)
        {
          var count = 0;
          count += viewModel.Section.Routines
            .Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key &&
                        x.EndTime.Ticks <= viewModel.BreakTime.StartTime.Ticks).OrderBy(x => x.StartTime).Count();
          foreach (var routine in viewModel.Section.Routines.Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key && x.EndTime.Ticks <= viewModel.BreakTime.StartTime.Ticks).OrderBy(x => x.StartTime).ToList())
          {
            viewModel.Routines.Add(new ShowSectionViewModel.Routine()
            {
              StartTime = routine.StartTime.Hours.ToString("D2") + ":" + routine.StartTime.Minutes.ToString("D2"),
              EndTime = routine.EndTime.Hours.ToString("D2") + ":" + routine.EndTime.Minutes.ToString("D2"),
              SubjectId = routine.Subject.Id,
              SubjectName = routine.Subject.SubjectName,
              TeacherId = routine.Teacher.Id,
              TeacherName = routine.Teacher.FullName,
              WeekDayName = wkdayModel.Value,
              RoutineId = routine.Id
            });
          }

          for (int k = viewModel.Section.Routines.Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key && x.EndTime.Ticks <= viewModel.BreakTime.StartTime.Ticks).OrderBy(x => x.StartTime).Count(); k < viewModel.BreakTimePosition; k++)
          {
            count++;
            viewModel.Routines.Add(new ShowSectionViewModel.Routine()
            {
              StartTime = "-",
              EndTime = string.Empty,
              SubjectId = 0,
              SubjectName = String.Empty,
              TeacherId = 0,
              TeacherName = String.Empty,
              WeekDayName = wkdayModel.Value,
              RoutineId = 0
            });
          }

          if (viewModel.Section.TotalClasses >= 5)
            viewModel.Routines.Add(new ShowSectionViewModel.Routine()
            {

              StartTime =  (viewModel.BreakTime != null)
                  ? viewModel.BreakTime.StartTime.Hours.ToString("D2") + ":" +
                    viewModel.BreakTime.StartTime.Minutes.ToString("D2")
                  : "-",
              EndTime = (viewModel.BreakTime != null)
                  ? viewModel.BreakTime.EndTime.Hours.ToString("D2") + ":" +
                    viewModel.BreakTime.EndTime.Minutes.ToString("D2")
                  : "-",
              SubjectId = 0,
              SubjectName = String.Empty,
              TeacherId = 0,
              TeacherName = String.Empty,
              WeekDayName = wkdayModel.Value,
              RoutineId = 0
            });

          foreach (var routine in viewModel.Section.Routines.Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key && x.StartTime.Ticks >= viewModel.BreakTime.EndTime.Ticks).OrderBy(x => x.StartTime).ToList())
          {
            count += viewModel.Section.Routines
              .Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key &&
                          x.StartTime.Ticks >= viewModel.BreakTime.EndTime.Ticks).OrderBy(x => x.StartTime).Count();
            viewModel.Routines.Add(new ShowSectionViewModel.Routine()
            {
              StartTime = routine.StartTime.Hours.ToString("D2") + ":" + routine.StartTime.Minutes.ToString("D2"),
              EndTime = routine.EndTime.Hours.ToString("D2") + ":" + routine.EndTime.Minutes.ToString("D2"),
              SubjectId = routine.Subject.Id,
              SubjectName = routine.Subject.SubjectName,
              TeacherId = routine.Teacher.Id,
              TeacherName = routine.Teacher.FullName,
              WeekDayName = wkdayModel.Value,
              RoutineId = routine.Id
            });
          }

          for (int k = count + 1; k <= viewModel.Section.TotalClasses; k++)
          {
            viewModel.Routines.Add(new ShowSectionViewModel.Routine()
            {
              StartTime = "-",
              EndTime = string.Empty,
              SubjectId = 0,
              SubjectName = String.Empty,
              TeacherId = 0,
              TeacherName = String.Empty,
              WeekDayName = wkdayModel.Value,
              RoutineId = 0
            });
          }
        }

      }

      if (viewModel.BreakTime == null)
      {
        foreach (var wkdayModel in viewModel.WeekDayDictonary)
        {
          var count = 0;
          count += viewModel.Section.Routines
            .Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key).OrderBy(x => x.StartTime).Take(viewModel.Section.TotalClasses / 2).Count();

          var firstpickList = viewModel.Section.Routines
            .Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key).OrderBy(x => x.StartTime)
            .Take(viewModel.Section.TotalClasses / 2).ToList();
          foreach (var routine in viewModel.Section.Routines.Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key).OrderBy(x => x.StartTime).ToList().Take(viewModel.Section.TotalClasses / 2))
          {
            viewModel.Routines.Add(new ShowSectionViewModel.Routine()
            {
              StartTime = routine.StartTime.Hours.ToString("D2") + ":" + routine.StartTime.Minutes.ToString("D2"),
              EndTime = routine.EndTime.Hours.ToString("D2") + ":" + routine.EndTime.Minutes.ToString("D2"),
              SubjectId = routine.Subject.Id,
              SubjectName = routine.Subject.SubjectName,
              TeacherId = routine.Teacher.Id,
              TeacherName = routine.Teacher.FullName,
              WeekDayName = wkdayModel.Value,
              RoutineId = routine.Id
            });
          }

          for (int k = viewModel.Section.Routines.Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key).OrderBy(x => x.StartTime).ToList().Take(viewModel.Section.TotalClasses / 2).Count(); k < viewModel.BreakTimePosition; k++)
          {
            count++;
            viewModel.Routines.Add(new ShowSectionViewModel.Routine()
            {
              StartTime = "-",
              EndTime = string.Empty,
              SubjectId = 0,
              SubjectName = String.Empty,
              TeacherId = 0,
              TeacherName = String.Empty,
              WeekDayName = wkdayModel.Value,
              RoutineId = 0
            });
          }

          if (viewModel.Section.TotalClasses >= 5)
            viewModel.Routines.Add(new ShowSectionViewModel.Routine()
            {

              StartTime = "-",
              EndTime = string.Empty,
              SubjectId = 0,
              SubjectName = String.Empty,
              TeacherId = 0,
              TeacherName = String.Empty,
              WeekDayName = wkdayModel.Value,
              RoutineId = 0
            });

          foreach (var routine in viewModel.Section.Routines.Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key).OrderBy(x => x.StartTime).ToList().Where(p => !firstpickList.Any(p2 => p2.Id == p.Id)))
          {
            count += viewModel.Section.Routines
              .Where(x => x.WeekDay == (DayOfWeek)wkdayModel.Key &&
                          x.StartTime.Ticks >= viewModel.BreakTime.EndTime.Ticks).OrderBy(x => x.StartTime).Count();
            viewModel.Routines.Add(new ShowSectionViewModel.Routine()
            {
              StartTime = routine.StartTime.Hours.ToString("D2") + ":" + routine.StartTime.Minutes.ToString("D2"),
              EndTime = routine.EndTime.Hours.ToString("D2") + ":" + routine.EndTime.Minutes.ToString("D2"),
              SubjectId = routine.Subject.Id,
              SubjectName = routine.Subject.SubjectName,
              TeacherId = routine.Teacher.Id,
              TeacherName = routine.Teacher.FullName,
              WeekDayName = wkdayModel.Value,
              RoutineId = routine.Id
            });
          }

          for (int k = count + 1; k <= viewModel.Section.TotalClasses; k++)
          {
            viewModel.Routines.Add(new ShowSectionViewModel.Routine()
            {
              StartTime = "-",
              EndTime = string.Empty,
              SubjectId = 0,
              SubjectName = String.Empty,
              TeacherId = 0,
              TeacherName = String.Empty,
              WeekDayName = wkdayModel.Value,
              RoutineId = 0
            });
          }
        }

      }

      return viewModel;
    }
  }
}
