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
  public class GetAttendanceListByDateRangeUseCase : IGetAttendanceListByDateRangeUseCase
  {
    private readonly IRepository _repository;

    public GetAttendanceListByDateRangeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public IEnumerable<GetAttendanceListByDateRangeResponseMessage.Attendance>
      GetEmployeeAttendanceListByDateRange(
        GetAttendanceListByDateRangeRequestMessage requestMessage)
    {
      var startDate = requestMessage.AttendanceStartDate;
      var endDate = requestMessage.AttendanceEndDate;
      IQueryable<Attendance> attendances;
      if (requestMessage.EmployeeId > 0)
      {
        attendances = _repository.TableNoTracking<Attendance>()
          .Include(nameof(Domain.Employees.Employee))
          .Where(x => x.Employee.Id == requestMessage.EmployeeId
                      && x.AttendanceDate <= endDate
                      && x.AttendanceDate >= startDate);
      }
      else
      {
        attendances = _repository.TableNoTracking<Attendance>()
          .Include(nameof(Student))
          .Include(nameof(Student) +"."+ nameof(BranchMedium))
          .Where(x => x.Student.Id == requestMessage.StudentId
                      && x.AttendanceDate <= endDate
                      && x.AttendanceDate >= startDate);
      }

      var dates = Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
        .Select(offset => startDate.AddDays(offset))
        .ToArray();

      var publicHolidaysOfCurrentYear = _repository.Table<PublicHoliday>()
        .Where(p => p.EndDate.Year == DateTime.Now.Year)
        .ToList();
      var eachDayOfHolidays = GetHolidays(publicHolidaysOfCurrentYear, requestMessage.AttendanceStartDate,
        requestMessage.AttendanceEndDate);
      return ToRequestCurrentMonthAttendance(attendances, dates, eachDayOfHolidays)
        .OrderByDescending(a => a.AttendanceDate);
    }

    private IEnumerable<GetAttendanceListByDateRangeResponseMessage.Attendance>
      ToRequestCurrentMonthAttendance(IQueryable<Attendance> attendances, DateTime[] dates,
        List<Holiday> eachHolidaysList)
    {
      var requestAttendanceList = new List<GetAttendanceListByDateRangeResponseMessage.Attendance>();
      foreach (var day in dates)
      {
        var attendance = GetAttendance(day, attendances);

        if (attendance != null)
        {
          if (attendance.Employee != null)
            requestAttendanceList.Add(new GetAttendanceListByDateRangeResponseMessage.Attendance
            {
              EmployeeId = attendance.Employee.Id,
              AttendanceId = attendance.Id,
              AttendanceDate = attendance.AttendanceDate,
              InTime = attendance.InTime,
              OutTime = attendance.OutTime
            });
          else
            requestAttendanceList.Add(new GetAttendanceListByDateRangeResponseMessage.Attendance
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
          requestAttendanceList.Add(new GetAttendanceListByDateRangeResponseMessage.Attendance
          {
            AttendanceDate = day
          });
        }
      }

      var attendanceListWithHolidayBooleanData =
        GetAttendanceListWithHolidayBooleanData(requestAttendanceList, eachHolidaysList);
      return attendanceListWithHolidayBooleanData;
    }

    private IEnumerable<GetAttendanceListByDateRangeResponseMessage.Attendance>
      GetAttendanceListWithHolidayBooleanData(
        List<GetAttendanceListByDateRangeResponseMessage.Attendance> requestAttendanceList,
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

    private Attendance GetAttendance(DateTime dayOfMonth,
      IQueryable<Attendance> attendances)
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

      return holidays.Where(h => h.Date <= attendanceEndDate && h.Date >= attendanceStartDate).ToList();
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

    private class Holiday
    {
      public DateTime Date { get; set; }
      public string HolidayName { get; set; }
    }
  }
}