using System;
using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class GetBillingDataResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public Billing Billings { get; }
    public GetBillingDataResponseMessage(ValidationResult validationResult, Billing billing)
    {
      ValidationResult = validationResult;
      Billings = billing;
    }
    
    public class Billing
    {
      public List<BillingInfo> BillingInfos { get; set; }
      public List<int> Years { get; set; }
      public decimal PendingAmount { get; set; }
    }
    public class BillingInfo
    {
      public string TransactionNo { get; set; }
      public string UserCode { get; set; }
      public int Year { get; set; }
      public MonthsOfYearEnum Month {get;set;}
      public DateTime PaymentDate { get; set; }
      public decimal PerStudentCharge { get; set; }
      public int TotalStudents { get; set; }
      public decimal Amount { get; set; }
      public decimal Vat { get; set; }
      public PaymentMethod PaymentMethod { get; set; }
      public bool IsProcessed { get; set; }
    }
  }
}