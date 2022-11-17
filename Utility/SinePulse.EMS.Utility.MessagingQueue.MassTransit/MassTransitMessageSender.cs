using System.Threading.Tasks;

namespace SinePulse.EMS.Utility.MessagingQueue.MassTransit
{
  public class MassTransitMessageSender : IMessageSender
  {
    private readonly IEndpointProvider _endpointProvider;

    public MassTransitMessageSender(IEndpointProvider endpointProvider)
    {
      _endpointProvider = endpointProvider;
    }

    public async Task Send(object message, string address)
    {
      var endpoint = await _endpointProvider.GetEndpoint(address);
      await endpoint.Send(message);
    }
  }
}