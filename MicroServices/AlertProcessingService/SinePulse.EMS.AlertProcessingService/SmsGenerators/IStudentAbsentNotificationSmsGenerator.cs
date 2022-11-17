using System;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public interface IStudentAbsentNotificationSmsGenerator
  {
    Task<SmsMessage> GenerateStudentAbsentNotificationSms(ContactPersonData guardian, Student student,
      TimeSpan startTime, TimeSpan absentTimeBoundary, DateTime date, string schoolName);
  }
}