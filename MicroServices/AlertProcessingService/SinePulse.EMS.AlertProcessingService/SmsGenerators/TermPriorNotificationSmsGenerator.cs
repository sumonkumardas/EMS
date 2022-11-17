using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public class TermPriorNotificationSmsGenerator : ITermPriorNotificationSmsGenerator
  {
    public Task<SmsMessage> GenerateTermPriorNotificationSms(ContactPersonData guardian, Student student,
      string termName, string sessionName, DateTime startingDate, string schoolName)
    {
      var smsMessage = new SmsMessage
      {
        PhoneNumbers = new List<string> {guardian.PhoneNo},
        Message =
          $"Mr/Mrs {guardian.Name}, {termName}'s exam of {sessionName} will be started from {startingDate.Date.ToLongDateString()}. We are expecting your son/daughter {student.FullName}'s participation test.\n- {schoolName}"
      };

      return Task.FromResult(smsMessage);
    }
  }
}