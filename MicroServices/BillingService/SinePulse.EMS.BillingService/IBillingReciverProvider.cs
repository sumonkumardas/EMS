using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.BillingService
{
  public interface IBillingReceiverProvider
  {
    IEnumerable<object> GetReceivers();
  }
}
