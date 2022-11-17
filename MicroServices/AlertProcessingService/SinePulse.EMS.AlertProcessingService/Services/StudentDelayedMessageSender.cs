using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.AlertProcessingService.SmsGenerators;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class StudentDelayedMessageSender : IStudentDelayedMessageSender
  {
    private readonly IRepository _repository;
    private readonly IStudentDelayedDecider _studentDelayedDecider;
    private readonly IContactPersonProvider _contactPersonProvider;
    private readonly IMessageSender _messageSender;
    private readonly IStudentDelayedNotificationSmsGenerator _studentDelayedNotificationSmsGenerator;

    public StudentDelayedMessageSender(IRepository repository, IStudentDelayedDecider studentDelayedDecider,
      IContactPersonProvider contactPersonProvider, IMessageSender messageSender,
      IStudentDelayedNotificationSmsGenerator studentDelayedNotificationSmsGenerator)
    {
      _repository = repository;
      _studentDelayedDecider = studentDelayedDecider;
      _contactPersonProvider = contactPersonProvider;
      _messageSender = messageSender;
      _studentDelayedNotificationSmsGenerator = studentDelayedNotificationSmsGenerator;
    }

    public async Task StudentDelayedMessage(long branchMediumId)
    {
      var branchMedium = await _repository.GetByIdAsync<BranchMedium, Shift, Institute>(
        branchMediumId,
        x => x.Shift,
        x => x.Branch.Institute);
      var students = await _repository.Filter<Student>(x => x.BranchMedium.Id == branchMediumId).ToListAsync();
      foreach (var student in students)
      {
        if (await _studentDelayedDecider.IsStudentDelayed(student.Id, DateTime.Today, branchMedium.Shift.StartTime))
        {
          foreach (var contactPerson in await _contactPersonProvider.GetContactPersons(student.Id))
          {
            var smsMessage = await _studentDelayedNotificationSmsGenerator.GenerateStudentDelayedNotificationSms(
              contactPerson, student, branchMedium.Shift.StartTime, DateTime.Today,
              branchMedium.Branch.Institute.OrganisationName);
            await _messageSender.Send(smsMessage, MicroServiceAddresses.SmsSendingService);
          }
        }
      }
    }
  }
}