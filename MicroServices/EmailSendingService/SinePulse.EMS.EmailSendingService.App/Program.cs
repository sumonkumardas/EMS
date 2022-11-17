using System;
using Topshelf;

namespace SinePulse.EMS.EmailSendingService.App
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var rc = HostFactory.Run(x =>
      {
        x.Service<EmailSendingServiceInstance>(s =>
        {
          s.ConstructUsing(name => new EmailSendingService.Externals.EmailSendingServiceInstance());
          s.WhenStarted(async tc => await tc.Start());
          s.WhenStopped(async tc => await tc.Stop());
        });
        x.RunAsLocalSystem();

        x.SetDescription("SinePulse Education Management System - Email Sending Service");
        x.SetDisplayName("SinePulse Education Management System - Email Sending Service");
        x.SetServiceName("SpEmsEmailSendingService");
      });

      var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
      Environment.ExitCode = exitCode;
    }
  }
}