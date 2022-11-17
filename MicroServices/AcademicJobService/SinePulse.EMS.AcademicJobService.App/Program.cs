using System;
using Topshelf;

namespace SinePulse.EMS.AcademicJobService.App
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var rc = HostFactory.Run(x =>
      {
        x.Service<AcademicJobServiceInstance>(s =>
        {
          s.ConstructUsing(name => new Externals.AcademicJobServiceInstance());
          s.WhenStarted(async tc => await tc.Start());
          s.WhenStopped(async tc => await tc.Stop());
        });
        x.RunAsLocalSystem();

        x.SetDescription("SinePulse Education Management System - Academic Job Service");
        x.SetDisplayName("SinePulse Education Management System - Academic Job Service");
        x.SetServiceName("SpEmsAcademicJobService");
      });

      var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
      Environment.ExitCode = exitCode;
    }
  }
}