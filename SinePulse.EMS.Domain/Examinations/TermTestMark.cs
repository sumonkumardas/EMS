using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;

namespace SinePulse.EMS.Domain.Examinations
{
  public class TermTestMark : BaseEntity
  {
    public override string Title()
    {
      return string.IsNullOrWhiteSpace(Remarks)
        ? $"{ObtainedMark + GraceMark}"
        : $"{ObtainedMark + GraceMark} [{Remarks}]";
    }

    #region Primitive Properties

    public virtual decimal ObtainedMark { get; set; }

    public virtual decimal GraceMark { get; set; }

    public virtual string Remarks { get; set; }

    public virtual StatusEnum Status { get; set; }

    #endregion

    #region Complex Properties

    public virtual AuditFields AuditFields { get; set; } = new AuditFields();

    #endregion

    #region  Navigation Properties

    public virtual TermTest TermTest { get; set; }

    public virtual StudentSection StudentSection { get; set; }

    #endregion
  }
}