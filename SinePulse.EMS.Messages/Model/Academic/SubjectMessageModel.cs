using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class SubjectMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string SubjectName { get; set; }
    public int SubjectCode { get; set; }
    public StatusEnum Status { get; set; }
    #endregion
  }
}