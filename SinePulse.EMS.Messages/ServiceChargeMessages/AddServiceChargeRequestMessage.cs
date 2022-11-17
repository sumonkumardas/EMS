using MediatR;
using System;

namespace SinePulse.EMS.Messages.ServiceChargeMessages
{
  public class AddServiceChargeRequestMessage : IRequest<AddServiceChargeResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public decimal PerStudentCharge { get; set; }
    public int PaymentBufferPeriod { get; set; }
    public DateTime EffectiveDate { get; set; }
    public string CurrentUserName { get; set; }
  }
}
