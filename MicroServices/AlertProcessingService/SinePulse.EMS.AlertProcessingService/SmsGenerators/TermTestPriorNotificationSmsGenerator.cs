using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public class TermTestPriorNotificationSmsGenerator : ITermTestPriorNotificationSmsGenerator
  {
    public Task<SmsMessage> GenerateTermTestNotificationSms(ContactPersonData guardian, Student student,
      string termName, string sessionName, string subjectName, DateTime examTime, string schoolName)
    {
      var smsMessage = new SmsMessage
      {
        PhoneNumbers = new List<string> {guardian.PhoneNo},
        Message =
          $"Mr/Mrs {guardian.Name}, {termName}'s exam of {subjectName} in {sessionName} will be started from {examTime.ToLongDateString()} at {examTime.ToLongTimeString()}. We are expecting your son/daughter {student.FullName}'s participation in the test.\n- {schoolName}"
      };

      return Task.FromResult(smsMessage);
    }
  }
}