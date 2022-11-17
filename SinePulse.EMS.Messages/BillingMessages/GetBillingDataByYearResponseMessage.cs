using System;
using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class GetBillingDataByYearResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public Billing Billings { get; }
    public GetBillingDataByYearResponseMessage(ValidationResult validationResult, Billing billing)
    {
      ValidationResult = validationResult;
      Billings = billing;
    }
    
    public class Billing
    {
      public List<BillingInfo> BillingInfos { get; set; }
    }
    public class BillingInfo
    {
      public string TransactionNo { get; set; }
      public string UserCode { get; set; }
      public int Year { get; set; }
      public string Month {get;set;}
      public string PaymentDate { get; set; }
      public string PerStudentCharge { get; set; }
      public int TotalStudents { get; set; }
      public string Amount { get; set; }
      public string Vat { get; set; }
      public string PaymentMethod { get; set; }
      public bool IsProcessed { get; set; }
    }
  }
}