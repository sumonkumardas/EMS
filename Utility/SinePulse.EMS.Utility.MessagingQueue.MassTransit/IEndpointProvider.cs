using System.Threading.Tasks;
using MassTransit;

namespace SinePulse.EMS.Utility.MessagingQueue.MassTransit
{
  public interface IEndpointProvider
  {
    Task<ISendEndpoint> GetEndpoint(string address);
  }
}