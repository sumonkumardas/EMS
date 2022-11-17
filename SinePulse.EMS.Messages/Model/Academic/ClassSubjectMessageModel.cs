using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class ClassSubjectMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public GroupEnum Group { get; set; }
    public int FullMarks { get; set; }
    public int PassMarks { get; set; }
    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public ClassMessageModel Class { get; set; }
    public SubjectMessageModel Subject { get; set; }

    #endregion
  }
}