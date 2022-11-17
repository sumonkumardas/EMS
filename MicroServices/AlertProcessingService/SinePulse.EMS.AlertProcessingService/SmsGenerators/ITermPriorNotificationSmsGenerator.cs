using System;
using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public interface ITermPriorNotificationSmsGenerator
  {
    Task<SmsMessage> GenerateTermPriorNotificationSms(ContactPersonData guardian, Student student,
      string termName, string sessionName, DateTime startingDate, string schoolName);
  }
}