using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.AcademicJobService.MessageHandlers;
using SinePulse.EMS.AcademicJobService.Services;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;
using SinePulse.EMS.Utility.MessagingQueue.MassTransit;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace SinePulse.EMS.AcademicJobService.Externals
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

      container.RegisterType<IResultSheetPreparer, ResultSheetPreparer>();
      container.RegisterType<ISectionResultSheetPreparer, SectionResultSheetPreparer>();
      container.RegisterType<IClassResultSheetPreparer, ClassResultSheetPreparer>();
      container.RegisterType<PrepareResultSheetMessageHandler>();
      container.RegisterType<PrepareSectionResultSheetMessageHandler>();
      container.RegisterType<PrepareClassResultSheetMessageHandler>();

      Bus = busCreator.CreateBus((cfg, host) =>
      {
        cfg.ReceiveEndpoint(host, MicroServiceAddresses.AcademicJobService,
          e =>
          {
            e.Consumer(() => new MassTransit.MessageHandlers.PrepareResultSheetMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.PrepareSectionResultSheetMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.PrepareClassResultSheetMessageHandler(container));
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