using MediatR;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Messages.FeeCollection
{
  public class FeeCollectionRequestMessage : IRequest<FeeCollectionResponseMessage>
  {
    public decimal TotalAmount { get; set; }
    public decimal[] Amounts { get; set; }
    public decimal[] Waivers { get; set; }
    public long BankAccountAccountHeadId { get; set; }
    public long SessionId { get; set; }
    public long StudentId { get; set; }
    public long SectionId { get; set; }
    public AcademicFeePeriodEnum FeeType { get; set; }
    public MonthType Month { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string CurrentUserName { get; set; }
    public long BranchMediumId { get; set; }
    public long ClassId { get; set; }
    public int Roll { get; set; }
  }
}