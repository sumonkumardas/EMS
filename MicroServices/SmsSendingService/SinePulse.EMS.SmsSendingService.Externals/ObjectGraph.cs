using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.SmsSendingService.MessageHandlers;
using SinePulse.EMS.Utility.MessagingQueue;
using SinePulse.EMS.Utility.MessagingQueue.MassTransit;
using SinePulse.EMS.Utility.SmsSending;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace SinePulse.EMS.SmsSendingService.Externals
{
  public class ObjectGraph
  {
    private const string ConnectionString = "Data Source=WINDOWS-907BS36\\NEONMSSQLSERVER;Initial Catalog=Ems;User ID=sa;Password=123@123";
    public IBusControl Bus { get; private set; }

    public async Task Initialize()
    {
      var busCreator = new RabbitMqBusCreator(new RabbitMqConfig
      {
        Username = "sinepulse.ems.admin",
        Password = "sinepulse.ems.admin",
        ServerHost = "localhost",
        RabbitMqVirtualHost = "SinePulse.EducationManagementSystem"
      });

      var container = new UnityContainer();

      container.RegisterInstance(ConnectionString);
      container.RegisterType<ISchemaInitializer, SchemaInitializer>();
      container.RegisterType<EmsDbContext>(new PerResolveLifetimeManager(), new InjectionFactory(c =>
        new EmsDbContext(ConnectionString, c.Resolve<ISchemaInitializer>())));
      container.RegisterType<IRepository, GenericRepository>();

      container.RegisterType<ISmsSender, RobiSmsSender>();
      
      container.RegisterType<SmsMessageHandler>();

      Bus = busCreator.CreateBus((cfg, host) =>
      {
        cfg.ReceiveEndpoint(host, MicroServiceAddresses.SmsSendingService,
          e =>
          {
            e.Consumer(() => new MassTransit.MessageHandlers.SmsMessageHandler(container));
          });
      });

      container.RegisterInstance(Bus);
      container.RegisterInstance<IBus>(Bus);

      container.RegisterType<IUriGenerator>(new InjectionFactory(c =>
        new UriGenerator(busCreator.GetRabbitMqUri())));
      container.RegisterType<IEndpointProvider, EndpointProvider>();
      container.RegisterType<IMessageSender, MassTransitMessageSender>();
    }
  }
}