using System.Threading.Tasks;

namespace SinePulse.EMS.Utility.MessagingQueue
{
  public interface IMessageSender
  {
    Task Send(object message, string address);
  }
}