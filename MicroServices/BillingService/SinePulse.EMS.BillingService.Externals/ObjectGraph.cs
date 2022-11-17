using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using log4net;
using MassTransit;
using Quartz;
using Quartz.Impl;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.Utility.MessagingQueue;
using SinePulse.EMS.Utility.MessagingQueue.MassTransit;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace SinePulse.EMS.BillingService.Externals
{
  public class ObjectGraph
  {
    private const string ConnectionString = "Data Source=WINDOWS-907BS36\\NEONMSSQLSERVER;Initial Catalog=Ems;User ID=sa;Password=123@123";
    public Timer Timer { get; private set; }
    public BillingJobServiceApplication BillingJobServiceApplication { get; private set; }
    public IScheduler Scheduler { get; private set; }
    public IBusControl Bus { get; private set; }
    public async Task Initialize(ILog logger)
    {
      var container = new UnityContainer();

      container.RegisterInstance(ConnectionString);
      container.RegisterType<ISchemaInitializer, SchemaInitializer>();
      container.RegisterType<EmsDbContext>(new PerResolveLifetimeManager(), new InjectionFactory(c =>
        new EmsDbContext(ConnectionString, c.Resolve<ISchemaInitializer>())));
      container.RegisterType<IRepository, GenericRepository>();
      container.RegisterType<IBillingReceiverProvider, BillingReceiverProvider>();
      container.RegisterType<IMidNightTaskRunner, MidNightTaskRunner>();
      container.RegisterType<IDueBillChecker, DueBillChecker>();
      container.RegisterType<ICurrentSessionProvider, CurrentSessionProvider>();
      //container.RegisterType<ScheduleBillingAlertMessageHandler>();
      var busCreator = new RabbitMqBusCreator(new RabbitMqConfig
      {
        Username = "sinepulse.ems.admin",
        Password = "sinepulse.ems.admin",
        ServerHost = "localhost",
        RabbitMqVirtualHost = "SinePulse.EducationManagementSystem"
      });
      
        Bus = busCreator.CreateBus((cfg, host) =>
        {
          
        });
        container.RegisterInstance(Bus);
        container.RegisterInstance<IBus>(Bus);
        var breakTime = TimeSpan.FromSeconds(40);
        Timer = new Timer(breakTime.TotalMilliseconds);
        container.RegisterInstance(Timer);

        container.RegisterType<BillingJobServiceApplication>();

        Scheduler = await new StdSchedulerFactory().GetScheduler();

        container.RegisterInstance(Scheduler);

        container.RegisterType<IUriGenerator>(new InjectionFactory(c =>
          new UriGenerator(busCreator.GetRabbitMqUri())));
        container.RegisterType<IEndpointProvider, EndpointProvider>();
        container.RegisterType<IMessageSender, MassTransitMessageSender>();
        container.RegisterInstance(logger);
      BillingJobServiceApplication = container.Resolve<BillingJobServiceApplication>();
      

      StaticObjects.MessageSender = container.Resolve<IMessageSender>();
    }
  }
}
