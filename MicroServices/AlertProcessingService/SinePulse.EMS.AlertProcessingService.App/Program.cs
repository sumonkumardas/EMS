using System;
using Topshelf;

namespace SinePulse.EMS.AlertProcessingService.App
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var rc = HostFactory.Run(x =>
      {
        x.Service<AlertProcessingServiceInstance>(s =>
        {
          s.ConstructUsing(name => new AlertProcessingService.Externals.AlertProcessingServiceInstance());
          s.WhenStarted(async tc => await tc.Start());
          s.WhenStopped(async tc => await tc.Stop());
        });
        x.RunAsLocalSystem();

        x.SetDescription("SinePulse Education Management System - Alert Processing Service");
        x.SetDisplayName("SinePulse Education Management System - Alert Processing Service");
        x.SetServiceName("SpEmsAlertProcessingService");
      });

      var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
      Environment.ExitCode = exitCode;
    }
  }
}