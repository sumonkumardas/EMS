using SinePulse.EMS.Messages.Model.Enums;


namespace SinePulse.EMS.Messages.Model.Shared
{
  public class DesignationMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string DesignationName { get; set; }
    public EmployeeTypeEnum EmployeeType { get; set; }
    public StatusEnum Status { get; set; }
    #endregion
  }
}
