using System.Threading.Tasks;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SmsSendingMessages;

namespace SinePulse.EMS.AlertProcessingService.SmsGenerators
{
  public interface IResultSheetPreparedNotificationSmsGenerator
  {
    Task<SmsMessage> GenerateResultPreparedNotificationSms(ContactPersonData guardian, Student student,
      decimal gpa, string termName, string sessionName, string schoolName);
  }
}