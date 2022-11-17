using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Students
{
  public class StudentSectionMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public int RollNo { get; set; }
    public GroupEnum Group { get; set; }

    #endregion

    #region  Navigation Properties

    public StudentSectionMessageModel Student { get; set; }
    public SectionMessageModel Section { get; set; }
    public ClassMessageModel Class { get; set; }

    #endregion
  }
}