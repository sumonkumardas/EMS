using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public class StudentDelayedNotificationSmsGenerator : IStudentDelayedNotificationSmsGenerator
  {
    public Task<SmsMessage> GenerateStudentDelayedNotificationSms(ContactPersonData guardian, Student student,
      TimeSpan startTime, DateTime date, string schoolName)
    {
      var smsMessage = new SmsMessage
      {
        PhoneNumbers = new List<string> {guardian.PhoneNo},
        Message =
          $"Mr/Mrs {guardian.Name}, Your son/daughter {student.FullName} could not entered in school within {startTime:h:mm tt}  on {date.ToLongDateString()}.\n- {schoolName}"
      };

      return Task.FromResult(smsMessage);
    }
  }
}