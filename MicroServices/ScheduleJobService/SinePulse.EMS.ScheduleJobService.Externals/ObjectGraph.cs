using System;
using System.Threading.Tasks;
using System.Timers;
using MassTransit;
using Quartz;
using Quartz.Impl;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.ScheduleJobService.MessageHandlers;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.Utility.MessagingQueue;
using SinePulse.EMS.Utility.MessagingQueue.MassTransit;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace SinePulse.EMS.ScheduleJobService.Externals
{
  public class ObjectGraph
  {
    private const string ConnectionString = "Data Source=WINDOWS-907BS36\\NEONMSSQLSERVER;Initial Catalog=Ems;User ID=sa;Password=123@123";
    public IBusControl Bus { get; private set; }
    public Timer Timer { get; private set; }
    public ScheduleJobServiceApplication ScheduleJobServiceApplication { get; private set; }
    public IScheduler Scheduler { get; private set; }

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
      container.RegisterType<IClosingCandidateSessionProvider, ClosingCandidateSessionProvider>();
      container.RegisterType<ICurrentSessionProvider, CurrentSessionProvider>();
      container.RegisterType<IMidNightTaskRunner, MidNightTaskRunner>();

      container.RegisterType<ScheduleCheckStudentDelayedAlertMessageHandler>();
      container.RegisterType<ScheduleCheckStudentAbsentAlertMessageHandler>();
      container.RegisterType<ScheduleTermStartedAlertMessageHandler>();
      container.RegisterType<ScheduleTermTestStartedAlertMessageHandler>();
      container.RegisterType<ScheduleClassTestStartedAlertMessageHandler>();

      Bus = busCreator.CreateBus((cfg, host) =>
      {
        cfg.ReceiveEndpoint(host, MicroServiceAddresses.ScheduleJobService,
          e =>
          {
            e.Consumer(() => new MassTransit.MessageHandlers.ScheduleCheckStudentDelayedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.ScheduleCheckStudentAbsentAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.ScheduleTermStartedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.ScheduleTermTestStartedAlertMessageHandler(container));
            e.Consumer(() => new MassTransit.MessageHandlers.ScheduleClassTestStartedAlertMessageHandler(container));
          });
      });

      container.RegisterInstance(Bus);
      container.RegisterInstance<IBus>(Bus);

      var breakTime = TimeSpan.FromSeconds(40);
      Timer = new Timer(breakTime.TotalMilliseconds);
      container.RegisterInstance(Timer);

      container.RegisterType<ScheduleJobServiceApplication>();
      
      Scheduler = await new StdSchedulerFactory().GetScheduler();

      container.RegisterInstance(Scheduler);

      container.RegisterType<IUriGenerator>(new InjectionFactory(c =>
        new UriGenerator(busCreator.GetRabbitMqUri())));
      container.RegisterType<IEndpointProvider, EndpointProvider>();
      container.RegisterType<IMessageSender, MassTransitMessageSender>();

      ScheduleJobServiceApplication = container.Resolve<ScheduleJobServiceApplication>();

      StaticObjects.MessageSender = container.Resolve<IMessageSender>();
    }
  }
}