using System;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public interface IStudentDelayedNotificationSmsGenerator
  {
    Task<SmsMessage> GenerateStudentDelayedNotificationSms(ContactPersonData guardian, Student student,
      TimeSpan startTime, DateTime date, string schoolName);
  }
}