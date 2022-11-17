using System.Collections.Generic;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public class ResultSheetPreparedNotificationSmsGenerator : IResultSheetPreparedNotificationSmsGenerator
  {
    public Task<SmsMessage> GenerateResultPreparedNotificationSms(ContactPersonData guardian, Student student,
      decimal gpa, string termName, string sessionName, string schoolName)
    {
      var smsMessage = new SmsMessage
      {
        PhoneNumbers = new List<string> {guardian.PhoneNo},
        Message =
          $"Mr/Mrs {guardian.Name}, {termName}'s result of {sessionName} of your son/daughter {student.FullName} is prepared. S/he obtained GPA {gpa} in that term.\n- {schoolName}"
      };

      return Task.FromResult(smsMessage);
    }
  }
}