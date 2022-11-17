using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Domain.Holidays;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Attendances
{
  public class ShowCurrentMonthAttendanceListUseCase : IShowCurrentMonthAttendanceListUseCase
  {
    private readonly IRepository _repository;

    public ShowCurrentMonthAttendanceListUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public IEnumerable<ShowCurrentMonthAttendanceListResponseMessage.Attendance> ShowEmployeeAttendanceList(
      ShowCurrentMonthAttendanceListRequestMessage request)
    {
      var currentMonth = DateTime.Now.Month;
      var attendanceOfCurrentMonth = new List<Attendance>();
      if (request.EmployeeId > 0)
      {
        attendanceOfCurrentMonth = _repository.TableNoTracking<Attendance>()
          .Include(nameof(Domain.Employees.Employee))
          .Where(x => x.Employee.Id == request.EmployeeId
                      && x.AttendanceDate.Month == currentMonth)
          .ToList();
      }

      if (request.StudentId > 0)
      {
        attendanceOfCurrentMonth = _repository.TableNoTracking<Attendance>()
          .Include(nameof(Student))
          .Include(nameof(Student) +"."+ nameof(BranchMedium))
          .Where(x => x.Student.Id == request.StudentId
                      && x.AttendanceDate.Month == currentMonth)
          .ToList();
      }

      var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
      var today = DateTime.Today;

      var dates = Enumerable.Range(0, 1 + today.Subtract(firstDayOfMonth).Days)
        .Select(offset => firstDayOfMonth.AddDays(offset))
        .ToArray();

      var publicHolidays = _repository.Table<PublicHoliday>()
        .Where(p => p.StartDate >= firstDayOfMonth
                    && p.EndDate <= today)
        .ToList();
      var eachHolidays = GetHolidays(publicHolidays, request.AttendanceStartDate, request.AttendanceEndDate);
      return ToRequestCurrentMonthAttendance(attendanceOfCurrentMonth, dates, eachHolidays)
        .OrderByDescending(a => a.AttendanceDate);
    }

    private IEnumerable<ShowCurrentMonthAttendanceListResponseMessage.Attendance>
      ToRequestCurrentMonthAttendance(List<Attendance> attendances, DateTime[] dates,
        List<Holiday> eachHolidaysList)
    {
      var requestAttendanceList = new List<ShowCurrentMonthAttendanceListResponseMessage.Attendance>();
      foreach (var day in dates)
      {
        var attendance = GetAttendance(day, attendances);

        if (attendance != null)
        {
          if (attendance.Employee != null)
            requestAttendanceList.Add(new ShowCurrentMonthAttendanceListResponseMessage.Attendance
            {
              EmployeeId = attendance.Employee.Id,
              AttendanceId = attendance.Id,
              AttendanceDate = attendance.AttendanceDate,
              InTime = attendance.InTime,
              OutTime = attendance.OutTime
            });
          else
            requestAttendanceList.Add(new ShowCurrentMonthAttendanceListResponseMessage.Attendance
            {
              StudentId = attendance.Student.Id,
              BranchMediumId = attendance.Student.BranchMedium.Id,
              AttendanceId = attendance.Id,
              AttendanceDate = attendance.AttendanceDate,
              InTime = attendance.InTime,
              OutTime = attendance.OutTime
            });
        }
        else
        {
          requestAttendanceList.Add(new ShowCurrentMonthAttendanceListResponseMessage.Attendance
          {
            AttendanceDate = day
          });
        }
      }

      var attendanceListWithHolidayBooleanData =
        GetAttendanceListWithHolidayBooleanData(requestAttendanceList, eachHolidaysList);
      return attendanceListWithHolidayBooleanData;
    }

    private Attendance GetAttendance(DateTime dayOfMonth,
      List<Attendance> attendances)
    {
      return attendances.FirstOrDefault(attendance => attendance.AttendanceDate == dayOfMonth);
    }


    private List<Holiday> GetHolidays(List<PublicHoliday> publicHolidays, DateTime attendanceStartDate,
      DateTime attendanceEndDate)
    {
      var holidays = new List<Holiday>();
      foreach (var publicHoliday in publicHolidays)
      {
        if (publicHoliday.StartDate < publicHoliday.EndDate)
        {
          var dates = Enumerable.Range(0, 1 + publicHoliday.EndDate.Subtract(publicHoliday.StartDate).Days)
            .Select(offset => publicHoliday.StartDate.AddDays(offset))
            .ToArray();
          foreach (var date in dates)
          {
            holidays.Add(new Holiday
            {
              Date = date,
              HolidayName = publicHoliday.HolidayName
            });
          }
        }
        else
        {
          holidays.Add(new Holiday
          {
            Date = publicHoliday.StartDate,
            HolidayName = publicHoliday.HolidayName
          });
        }
      }

      return holidays;
    }

    private IEnumerable<ShowCurrentMonthAttendanceListResponseMessage.Attendance>
      GetAttendanceListWithHolidayBooleanData(
        List<ShowCurrentMonthAttendanceListResponseMessage.Attendance> requestAttendanceList,
        List<Holiday> holidaysList)
    {
      foreach (var attendance in requestAttendanceList)
      {
        if (IsHoliday(attendance.AttendanceDate, holidaysList))
        {
          attendance.IsPublicHoliday = true;
          attendance.HolidayName = holidaysList.FirstOrDefault(h => h.Date == attendance.AttendanceDate)?.HolidayName;
        }
        else
        {
          attendance.IsPublicHoliday = false;
        }
      }

      return requestAttendanceList;
    }

    private bool IsHoliday(DateTime day, List<Holiday> eachHolidaysList)
    {
      foreach (var holiday in eachHolidaysList)
      {
        if (day == holiday.Date)
        {
          return true;
        }
      }

      return false;
    }

    public class Holiday
    {
      public DateTime Date { get; set; }
      public string HolidayName { get; set; }
    }
  }
}