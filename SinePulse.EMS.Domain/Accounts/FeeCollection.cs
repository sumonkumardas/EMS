using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Domain.Accounts
{
  public class FeeCollection : BaseEntity
  {
    #region Primitive Properties

    public decimal Amount { get; set; }
    public decimal Fees { get; set; }
    public AcademicFeePeriodEnum FeeType { get; set; }
    public decimal Waiver { get; set; }
    public MonthType Month { get; set; }
    public string CollectedBy { get; set; }
    public DateTime CollectionDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string BankAccountNo { get; set; }

    #endregion

    #region Navigation Properties

    public Student Student { get; set; }
    public Session Session { get; set; }
    public Transaction Transaction { get; set; }

    #endregion
  }
}