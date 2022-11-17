using System.Threading.Tasks;
using MassTransit;
using SinePulse.EMS.AlertProcessingService.MessageHandlers;
using SinePulse.EMS.AlertProcessingService.Services;
using SinePulse.EMS.AlertProcessingService.SmsGenerators;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;
using SinePulse.EMS.Utility.MessagingQueue.MassTransit;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace SinePulse.EMS.AlertProcessingService.Externals
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

      container.RegisterType<IContactPersonProvider, ContactPersonProvider>();
      
      container.RegisterType<IStudentAbsentNotificationSmsGenerator, StudentAbsentNotificationSmsGenerator>();
      container.RegisterType<IStudentDelayedNotificationSmsGenerator, StudentDelayedNotificationSmsGenerator>();
      container.RegisterType<ITermPriorNotificationSmsGenerator, TermPriorNotificationSmsGenerator>();
      container.RegisterType<ITermTestPriorNotificationSmsGenerator, TermTestPriorNotificationSmsGenerator>();
      container.RegisterType<IClassTestPriorNotificationSmsGenerator, ClassTestPriorNotificationSmsGenerator>();
      container.RegisterType<IResultSheetPreparedNotificationSmsGenerator,
        ResultSheetPreparedNotificationSmsGenerator>();
      container.RegisterType<IMultipleStudentResultSheetPreparedMessageSender,
        MultipleStudentResultSheetPreparedMessageSender>();
      container.RegisterType<IStudentAbsentMessageSender, StudentAbsentMessageSender>();
      container.RegisterType<IStudentDelayedMessageSender, StudentDelayedMessageSender>();
      container.RegisterType<IClassTestStartedMessageSender, ClassTestStartedMessageSender>();
      container.RegisterType<ITermTestStartedMessageSender, TermTestStartedMessageSender>();
      container.RegisterType<ITermStartedMessageSender, TermStartedMessageSender>();
      container.RegisterType<ITermResultSheetPreparedMessageSender, TermResultSheetPreparedMessageSender>();
      container.RegisterType<IClassResultSheetPreparedMessageSender, ClassResultSheetPreparedMessageSender>();
      container.RegisterType<ISectionResultSheetPreparedMessageSender, SectionResultSheetPreparedMessageSender>();
      container.RegisterType<IStudentResultSheetPreparedMessageSender, StudentResultSheetPreparedMessageSender>();
      container.RegisterType<IStudentDelayedDecider, StudentInTimingDecider>();
      container.RegisterType<IStudentAbsentDecider, StudentInTimingDecider>();
      container.RegisterType<IStudentInTimingAlertScheduler, StudentInTimingAlertScheduler>();
      container.RegisterType<ITermStartedAlertScheduler, TermStartedAlertScheduler>();
      container.RegisterType<ITermStartedAlertScheduler, TermStartedAlertScheduler>();
      container.RegisterType<ITermTestStartedAlertScheduler, TermTestStartedAlertScheduler>();
      container.RegisterType<IClassTestStartedAlertScheduler, ClassTestStartedAlertScheduler>();

      container.RegisterType<IStudentDelayedNotificationSmsGenerator, StudentDelayedNotificationSmsGenerator>();

      container.RegisterType<ConfigureDailyAlertMessageHandler>();
      container.RegisterType<CheckStudentDelayedAlertMessageHandler>();
      container.RegisterType<CheckStudentAbsentAlertMessageHandler>();
      container.RegisterType<ConfigureTermStartedAlertMessageHandler>();
      container.RegisterType<ConfigureTermTestStartedAlertMessageHandler>();
      container.RegisterType<ConfigureClassTestStartedAlertMessageHandler>();
      container.RegisterType<TermStartedAlertMessageHandler>();
      container.RegisterType<TermTestStartedAlertMessageHandler>();
      container.RegisterType<ClassTestStartedAlertMessageHandler>();
      container.RegisterType<StudentResultSheetPreparedAlertMessageHandler>();
      container.RegisterType<SectionResultSheetPreparedAlertMessageHandler>();
      container.RegisterType<ClassResultSheetPreparedAlertMessageHandler>();
      container.RegisterType<TermResultSheetPreparedAlertMessageHandler>();

      Bus = busCreator.CreateBus((cfg, host) =>
      {
        cfg.ReceiveEndpoint(host, MicroServiceAddresses.AlertProcessingService,
          e =>
          {
            e.Consumer(() => new MassTransit.MessageHandlers.ConfigureDailyAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.CheckStudentDelayedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.CheckStudentAbsentAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.ConfigureTermStartedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.ConfigureTermTestStartedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.ConfigureClassTestStartedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.TermStartedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.TermTestStartedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.ClassTestStartedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.StudentResultSheetPreparedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.SectionResultSheetPreparedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.ClassResultSheetPreparedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.TermResultSheetPreparedAlertMessageHandler(container));
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