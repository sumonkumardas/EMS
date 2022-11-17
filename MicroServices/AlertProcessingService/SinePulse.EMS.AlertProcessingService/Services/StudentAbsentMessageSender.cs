using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.AlertProcessingService.SmsGenerators;
using SinePulse.EMS.AlertProcessingService.Utility;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Notification;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class StudentAbsentMessageSender : IStudentAbsentMessageSender
  {
    private readonly IRepository _repository;
    private readonly IStudentAbsentDecider _studentAbsentDecider;
    private readonly IContactPersonProvider _contactPersonProvider;
    private readonly IStudentAbsentNotificationSmsGenerator _studentAbsentNotificationSmsGenerator;

    public StudentAbsentMessageSender(IRepository repository, IStudentAbsentDecider studentAbsentDecider,
      IContactPersonProvider contactPersonProvider,
      IStudentAbsentNotificationSmsGenerator studentAbsentNotificationSmsGenerator)
    {
      _repository = repository;
      _studentAbsentDecider = studentAbsentDecider;
      _contactPersonProvider = contactPersonProvider;
      _studentAbsentNotificationSmsGenerator = studentAbsentNotificationSmsGenerator;
    }

    public async Task StudentAbsentMessage(long branchMediumId)
    {
      var branchMedium = await _repository.GetByIdAsync<BranchMedium, Shift, Institute>(
        branchMediumId,
        x => x.Shift,
        x => x.Branch.Institute);

      var notificationConfiguration = await _repository.Filter<NotificationConfiguration>(
        x => x.BranchMedium.Id == branchMedium.Id).FirstAsync();
      var absentTimeBoundary = TimeSpanUtility.ToTimeSpan(notificationConfiguration.AbsentPeriod,
        notificationConfiguration.AbsentTimeIntervalType);

      var students = await _repository.Filter<Student>(x => x.BranchMedium.Id == branchMediumId).ToListAsync();
      foreach (var student in students)
      {
        if (await _studentAbsentDecider.IsStudentAbsent(student.Id, DateTime.Today, branchMedium.Shift.StartTime,
          absentTimeBoundary))
        {
          foreach (var contactPerson in await _contactPersonProvider.GetContactPersons(student.Id))
          {
            await _studentAbsentNotificationSmsGenerator.GenerateStudentAbsentNotificationSms(contactPerson, student,
              branchMedium.Shift.StartTime, absentTimeBoundary, DateTime.Today,
              branchMedium.Branch.Institute.OrganisationName);
          }
        }
      }
    }
  }
}