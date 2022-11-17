using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Constants;
using SinePulse.EMS.EmailSendingService.MessageHandlers;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.EmailSending;
using SinePulse.EMS.Utility.MessagingQueue;
using SinePulse.EMS.Utility.MessagingQueue.MassTransit;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace SinePulse.EMS.EmailSendingService.Externals
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
      container.RegisterType<IEmailSender, EmailSender>();
      container.RegisterType<EmailMessageHandler>();

      Bus = busCreator.CreateBus((cfg, host) =>
      {
        cfg.ReceiveEndpoint(host, MicroServiceAddresses.EmailSendingService,
          e =>
          {
            e.Consumer(() => new MassTransit.MessageHandlers.EmailMessageHandler(container));
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