using System;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.OnlinePaymentMessages
{
  public class GetPaymentInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public PaymentInfo PaymentInfos { get; }
    
    public GetPaymentInfoResponseMessage(ValidationResult validationResult, PaymentInfo paymentInfos)
    {
      ValidationResult = validationResult;
      PaymentInfos = paymentInfos;
    }
    
    public class PaymentInfo
    {
      public long BranchMediumId { get; set; }
      public string TransactionId { get; set; }
      public string UserCode { get; set; }
      public int Year { get; set; }
      public MonthsOfYearEnum Month {get;set;}
      public DateTime PaymentDate { get; set; }
      public decimal PerStudentCharge { get; set; }
      public int TotalStudents { get; set; }
      public decimal TransactionAmount { get; set; }
      public decimal StoreAmount { get; set; }
      public string ValidationId { get; set; }
      public string CardType { get; set; }
    }
  }
}