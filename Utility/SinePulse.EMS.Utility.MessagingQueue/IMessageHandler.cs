using System.Threading.Tasks;

namespace SinePulse.EMS.Utility.MessagingQueue
{
  public interface IMessageHandler<in TMessage>
  {
    Task Handle(TMessage message);
  }

  public interface IMessageHandler<in TRequestMessage, TResponseMessage>
  {
    Task<TResponseMessage> Handle(TRequestMessage message);
  }
}