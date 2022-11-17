using System.Threading.Tasks;

namespace SinePulse.EMS.Utility.SmsSending
{
  public interface ISmsSender
  {
    Task<bool> SendSms(Sms sms);
  }
}