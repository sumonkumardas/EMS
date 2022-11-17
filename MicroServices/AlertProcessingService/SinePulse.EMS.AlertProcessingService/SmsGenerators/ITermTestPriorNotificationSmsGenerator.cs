using System;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public interface ITermTestPriorNotificationSmsGenerator
  {
    Task<SmsMessage> GenerateTermTestNotificationSms(ContactPersonData guardian, Student student,
      string termName, string sessionName, string subjectName, DateTime examTime, string schoolName);
  }
}