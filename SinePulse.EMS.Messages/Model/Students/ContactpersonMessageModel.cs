

using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.Messages.Model.Students;

namespace SinePulse.EMS.Messages.Model.Students
{
  public class ContactPersonMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public RelationTypeEnum RelationType { get; set; }
    public string Name { get; set; }
    public string PhoneNo { get; set; }
    public string EmailAddress { get; set; }
    public string NID { get; set; }
    public string Profession { get; set; }
    public string Designation { get; set; }
    public string OfficeNameAddress { get; set; }
    public string OfficeContactNo { get; set; }
    public StatusEnum Status { get; set; }
    public string ImageUrl { get; set; }
    #endregion

    #region  Navigation Properties
    public StudentMessageModel Student { get; set; }
    #endregion
  }
}
