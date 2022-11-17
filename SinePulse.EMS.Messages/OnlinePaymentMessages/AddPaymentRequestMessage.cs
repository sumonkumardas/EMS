using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.OnlinePaymentMessages
{
  public class AddPaymentRequestMessage : IRequest<AddPaymentResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public string TransactionId { get; set; }
    public string UserCode { get; set; }
    public decimal DueAmount { get; set; }
    public int Year { get; set; }
    public MonthsOfYearEnum Month { get; set; }
    public virtual decimal PerStudentCharge { get; set; }
    public virtual int TotalStudents { get; set; }
    public string CurrentUserName { get; set; }
  }
}