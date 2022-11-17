using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Examinations
{
  public class ExamRoutineMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string ExamRoutineName { get; set; }
    public TermMessageModel Term { get; set; }
    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public BranchMediumMessageModel BranchMedium { get; set; }

    #endregion
  }
}