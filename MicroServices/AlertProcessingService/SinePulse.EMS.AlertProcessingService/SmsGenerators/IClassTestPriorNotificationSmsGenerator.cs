using System;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public interface IClassTestPriorNotificationSmsGenerator
  {
    Task<SmsMessage> GenerateClassTestNotificationSms(ContactPersonData guardian, Student student, string termName,
      string sessionName, string subjectName, string classTestName, DateTime examTime, string schoolName);
  }
}