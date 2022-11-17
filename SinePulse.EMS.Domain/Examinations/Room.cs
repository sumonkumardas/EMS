using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Examinations
{
  public class Room : BaseEntity
  {
    #region Primitive Properties

    public virtual string RoomNo { get; set; }

    public virtual int ClassTimeCapacity { get; set; }

    public virtual int ExamTimeCapacity { get; set; }

    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties

    public virtual Branch Branch { set; get; }

    #endregion
  }
}