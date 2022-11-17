using System.Threading.Tasks;
using MassTransit;

namespace SinePulse.EMS.Utility.MessagingQueue.MassTransit
{
  public class EndpointProvider : IEndpointProvider
  {
    private readonly IBusControl _bus;
    private readonly IUriGenerator _uriGenerator;

    public EndpointProvider(IBusControl bus, IUriGenerator uriGenerator)
    {
      _bus = bus;
      _uriGenerator = uriGenerator;
    }

    public async Task<ISendEndpoint> GetEndpoint(string address)
    {
      var uri = _uriGenerator.GenerateUri(address);
      return await _bus.GetSendEndpoint(uri);
    }
  }
}