using MediatR;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Shared;
using System;

namespace SinePulse.EMS.Messages.ServiceChargeMessages
{
  public class EditServiceChargeRequestMessage : IRequest<ValidatedData<EditServiceChargeResponseMessage>>
  {
    public long ServiceChargeId { get; set; }
    public decimal PerStudentCharge { get; set; }
    public int PaymentBufferPeriod { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long BranchMediumId { get; set; }
    public string CurrentUserName { get; set; }
    public BranchMedium BranchMedium { get; set; }
  }
}
