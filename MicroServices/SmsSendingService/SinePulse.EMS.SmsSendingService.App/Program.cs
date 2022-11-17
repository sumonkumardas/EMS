using System;
using Topshelf;

namespace SinePulse.EMS.SmsSendingService.App
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var rc = HostFactory.Run(x =>
      {
        x.Service<SmsSendingServiceInstance>(s =>
        {
          s.ConstructUsing(name => new SmsSendingService.Externals.SmsSendingServiceInstance());
          s.WhenStarted(async tc => await tc.Start());
          s.WhenStopped(async tc => await tc.Stop());
        });
        x.RunAsLocalSystem();

        x.SetDescription("SinePulse Education Management System - Sms Sending Service");
        x.SetDisplayName("SinePulse Education Management System - Sms Sending Service");
        x.SetServiceName("SpEmsSmsSendingService");
      });

      var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
      Environment.ExitCode = exitCode;
    }
  }
}