using FluentValidation.Results;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class GetDueAmountResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public PendingInfo PendingInfos { get; }

    public GetDueAmountResponseMessage(ValidationResult validationResult, PendingInfo pendingInfos)
    {
      ValidationResult = validationResult;
      PendingInfos = pendingInfos;
    }
    
    public class PendingInfo
    {
      public string ActiveStudents { get; set; }
      public decimal PendingAmount { get; set; }
      public string PerStudentCharge { get; set; }
    }
  }
}