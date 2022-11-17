using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Examinations
{
  public class RoomMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string RoomNo { get; set; }

    public int ClassTimeCapacity { get; set; }

    public int ExamTimeCapacity { get; set; }

    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public BranchMessageModel Branch { set; get; }

    #endregion
  }
}