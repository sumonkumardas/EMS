using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class ClassMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string ClassName { get; set; }
    public int ClassCode { get; set; }
    public StatusEnum Status { get; set; }
    #endregion
  }
}
