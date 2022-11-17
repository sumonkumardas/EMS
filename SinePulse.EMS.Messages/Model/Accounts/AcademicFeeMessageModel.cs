using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Accounts
{
  public class AcademicFeeMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public decimal Fees { get; set; }
    public AcademicFeePeriodEnum FeePeriod { get; set; }

    #endregion

    #region  Navigation Properties

    public ClassMessageModel Class { get; set; }
    public BranchMediumAccountsHeadMessageModel AccountHead { get; set; }

    #endregion
  }
}