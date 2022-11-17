using System;
using Topshelf;

namespace SinePulse.EMS.FinancialJobService.App
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var rc = HostFactory.Run(x =>
      {
        x.Service<FinancialJobServiceInstance>(s =>
        {
          s.ConstructUsing(name => new Externals.FinancialJobServiceInstance());
          s.WhenStarted(async tc => await tc.Start());
          s.WhenStopped(async tc => await tc.Stop());
        });
        x.RunAsLocalSystem();

        x.SetDescription("SinePulse Education Management System - Financial Job Service");
        x.SetDisplayName("SinePulse Education Management System - Financial Job Service");
        x.SetServiceName("SpEmsFinancialJobService");
      });

      var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
      Environment.ExitCode = exitCode;
    }
  }
}