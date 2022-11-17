using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class MediumMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string MediumName { get; set; }
    public string MediumCode { get; set; }
    public StatusEnum Status { get; set; }
    #endregion
  }
}
