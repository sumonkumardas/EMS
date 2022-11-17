using SinePulse.EMS.Domain.Academic;
using System;

namespace SinePulse.EMS.Messages.ServiceChargeMessages
{
  public class DisplayEditServiceChargeResponseMessage
  {
    public ServiceCharge Charge { get; set; }

    public DisplayEditServiceChargeResponseMessage( ServiceCharge serviceCharge)
    {
      Charge = serviceCharge;
    }
    public class ServiceCharge
    {
      public long Id { get; set; }
      public decimal PerStudentCharge { get; set; }
      public int PaymentBufferPeriod { get; set; }
      public DateTime EffectiveDate { get; set; }
      public DateTime? EndDate { get; set; }
      public BranchMedium BranchMedium { get; set; }
    }
    
  }
}
