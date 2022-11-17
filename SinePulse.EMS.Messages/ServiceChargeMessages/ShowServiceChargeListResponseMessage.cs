using System;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.ServiceChargeMessages
{
  public class ShowServiceChargeListResponseMessage
  {
    public List<ServiceCharge> ServiceChargeData { get; }

    public ShowServiceChargeListResponseMessage(List<ServiceCharge> serviceCharge)
    {
      ServiceChargeData = serviceCharge;
    }
    public class ServiceCharge
    {
      public long Id { get; set; }
      public decimal PerStudentCharge { get; set; }
      public int PaymentBufferPeriod { get; set; }
      public DateTime EffectiveDate { get; set; }
      public DateTime? EndDate { get; set; }
      public string InstituteName { get; set; }
      public string BranchName { get; set; }
      public BranchMedium BranchMedium { get; set; }
    }

    public class BranchMedium
    {
      public long BranchMediumId { get; set; }
      public string BranchMediumName { get; set; }
    }

  }
}
