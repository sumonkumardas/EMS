using FluentValidation.Results;
using System;
namespace SinePulse.EMS.Messages.ServiceChargeMessages
{
  public class GetServiceChargeResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public ServiceCharge Charge { get; set; }

    public GetServiceChargeResponseMessage(ValidationResult validationResult, ServiceCharge serviceCharge)
    {
      ValidationResult = validationResult;
      Charge = serviceCharge;
    }

    public class ServiceCharge
    {
      public  long ServiceChargeId { get; set; }
      public decimal PerStudentCharge { get; set; }
      public int PaymentBufferPeriod { get; set; }
      public DateTime EffectiveDate { get; set; }
      public DateTime? EndDate { get; set; }
    }
  }
}
