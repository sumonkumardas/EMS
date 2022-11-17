using System;
using Topshelf;

namespace SinePulse.EMS.ScheduleJobService.App
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var rc = HostFactory.Run(x =>
      {
        x.Service<ScheduleJobServiceInstance>(s =>
        {
          s.ConstructUsing(name => new ScheduleJobService.Externals.ScheduleJobServiceInstance());
          s.WhenStarted(async tc => await tc.Start());
          s.WhenStopped(async tc => await tc.Stop());
        });
        x.RunAsLocalSystem();

        x.SetDescription("SinePulse Education Management System - Schedule Job Service");
        x.SetDisplayName("SinePulse Education Management System - Schedule Job Service");
        x.SetServiceName("SpEmsScheduleJobService");
      });

      var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
      Environment.ExitCode = exitCode;
    }
  }
}