using System;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace SinePulse.EMS.Utility.MessagingQueue.MassTransit
{
  public class RabbitMqBusCreator
  {
    private static IBusControl BusControl { get; set; }
    private static readonly object Lock = new object();
    private readonly RabbitMqConfig _config;

    public RabbitMqBusCreator(RabbitMqConfig config)
    {
      _config = config;
    }

    public IBusControl CreateBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
    {
      if (BusControl != null) return BusControl;
      lock (Lock)
      {
        return BusControl ?? CreateBusUnsafe(registrationAction);
      }
    }

    private IBusControl CreateBusUnsafe(
      Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
    {
      return Bus.Factory.CreateUsingRabbitMq(cfg =>
      {
        var host = cfg.Host(new Uri(GetRabbitMqUri()), hst =>
        {
          hst.Username(_config.Username);
          hst.Password(_config.Password);
        });

        registrationAction?.Invoke(cfg, host);
      });
    }

    public string GetRabbitMqUri()
    {
      return $"rabbitmq://{_config.ServerHost}/{_config.RabbitMqVirtualHost}/";
    }
  }
}