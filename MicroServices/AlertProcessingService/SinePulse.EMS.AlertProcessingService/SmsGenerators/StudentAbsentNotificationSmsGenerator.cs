using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public class StudentAbsentNotificationSmsGenerator : IStudentAbsentNotificationSmsGenerator
  {
    public Task<SmsMessage> GenerateStudentAbsentNotificationSms(ContactPersonData guardian, Student student,
      TimeSpan startTime, TimeSpan absentTimeBoundary, DateTime date, string schoolName)
    {
      var absentBoundaryEndTimestamp = startTime + absentTimeBoundary;
      var smsMessage = new SmsMessage
      {
        PhoneNumbers = new List<string> {guardian.PhoneNo},
        Message =
          $"Mr/Mrs {guardian.Name}, Your son/daughter {student.FullName} could not entered in school within {absentBoundaryEndTimestamp:h:mm tt} on {date.ToLongDateString()}. We are suspecting that s/he may absent in school today.\n- {schoolName}"
      };

      return Task.FromResult(smsMessage);
    }
  }
}