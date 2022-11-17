using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Students
{
  public class StudentWaiverMessageModel : BaseEntityMessageModel
  {
    #region Primitive Propertise

    public decimal Waiver { get; set; }
    public decimal Fees { get; set; }
    public bool InPercentage { get; set; }

    #endregion

    #region Navigation Properties

    public StudentMessageModel Student { get; set; }
    public ClassMessageModel Class { get; set; }
    public SectionMessageModel Section { get; set; }
    public BranchMediumAccountsHeadMessageModel AccountHead { get; set; }

    #endregion
  }
}