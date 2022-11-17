using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.BillingService
{
  public interface IMidNightTaskRunner
  {
    Task RunMidNightTask();
  }
}
