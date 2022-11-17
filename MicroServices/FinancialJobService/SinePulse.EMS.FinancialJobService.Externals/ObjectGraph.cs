using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.Constants;
using SinePulse.EMS.FinancialJobService.MessageHandlers;
using SinePulse.EMS.FinancialJobService.Services;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;
using SinePulse.EMS.Utility.MessagingQueue.MassTransit;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace SinePulse.EMS.FinancialJobService.Externals
{
  public class ObjectGraph
  {
    //Data Source=DESKTOP-N3ROI91\\SQLEXPRESS14;Initial Catalog=EMS;User ID=sa;Password=admin156271
    //Data Source=192.168.11.173\\MHDB;Initial Catalog=SinePulse_EMS;User ID=sa;Password=Admin1@34
    private const string ConnectionString = "Data Source=192.168.11.173\\MHDB;Initial Catalog=SinePulse_EMS;User ID=sa;Password=Admin1@34";

    public static TestMessageHandler TestMessageHandler { get; private set; }

    public IBusControl Bus { get; private set; }
    public IMessageSender MessageSender { get; set; }

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

      container.RegisterType<IDailyBalanceUpdater, DailyBalanceUpdater>();
      container.RegisterType<IYearClosingDataProvider, YearClosingDataProvider>();
      container.RegisterType<IYearCloser, YearCloser>();

      container.RegisterType<BalanceChangedNotificationMessageHandler>();
      container.RegisterType<RunYearClosingMessageHandler>();

      Bus = busCreator.CreateBus((cfg, host) =>
      {
        cfg.ReceiveEndpoint(host, MicroServiceAddresses.FinancialJobService,
          e =>
          {
            e.Consumer<MassTransit.MessageHandlers.TestMessageHandler>();
            e.Consumer(() => new MassTransit.MessageHandlers.BalanceChangedNotificationMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.RunYearClosingMessageHandler(container));
          });
      });

      container.RegisterInstance(Bus);
      container.RegisterInstance<IBus>(Bus);

      TestMessageHandler = new TestMessageHandler();

      var uriGenerator = new UriGenerator(busCreator.GetRabbitMqUri());
      var endpointProvider = new EndpointProvider(Bus, uriGenerator);
      MessageSender = new MassTransitMessageSender(endpointProvider);
    }
  }
}