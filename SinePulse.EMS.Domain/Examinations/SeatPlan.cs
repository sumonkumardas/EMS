using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Examinations
{
  public class SeatPlan : BaseEntity
  {
    #region Primitive Properties

    public virtual string RollRange { get; set; }
    public virtual int TotalStudent { get; set; }
    public virtual long RoomId { get; set; }
    public virtual long TestId { get; set; }
    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties

    public virtual Room Room { get; set; }
    public virtual TermTest Test { get; set; }

    #endregion
  }
}