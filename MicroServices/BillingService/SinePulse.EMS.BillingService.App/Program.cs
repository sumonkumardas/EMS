using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Topshelf;

namespace SinePulse.EMS.BillingService.App
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var logger = GetLogger();
      var rc = HostFactory.Run(x =>
      {
        x.Service<BillingServiceInstance>(s =>
        {
          s.ConstructUsing(name => new Externals.BillingServiceInstance(logger));
          s.WhenStarted(async tc => await tc.Start());
          s.WhenStopped(async tc => await tc.Stop());
        });
        x.RunAsLocalSystem();

        x.SetDescription("SinePulse Education Management System - Billing Service");
        x.SetDisplayName("SinePulse Education Management System - Billing Service");
        x.SetServiceName("SpEmsBillingJobService");

        
      });

      var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
      Environment.ExitCode = exitCode;
    }

    private static ILog GetLogger()
    {
      var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

      XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

      return LogManager.GetLogger(typeof(Program));
    }
  }
}
