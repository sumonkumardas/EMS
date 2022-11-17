using System;
using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.BillingMessages
{
  public class AddBillingDataRequestMessage : IRequest<AddBillingDataResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public string CurrentUserName { get; set; }
    public string TransactionId { get; set; }
    public string UserCode { get; set; }
    public int Year { get; set; }
    public MonthsOfYearEnum Month { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal PerStudentCharge { get; set; }
    public int TotalStudents { get; set; }
    public decimal Amount { get; set; }
    public decimal Vat { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public bool IsProcessed { get; set; }
  }
}