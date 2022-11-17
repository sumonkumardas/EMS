using System;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Students
{
  public class StudentMessageModel : BaseEntityMessageModel
  {
    public string Title()
    {
      return FullName + " (" + StudentId + ")";
    }

    #region Primitive Properties

    public string FullName { get; set; }
    public string StudentId { get; set; }
    public RelationTypeEnum Guardian { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public StatusEnum Status { get; set; }
    public string ImageUrl { get; set; }

    #endregion

    #region  Navigation Properties

    public AddressMessageModel PresentAddress { get; set; }
    public AddressMessageModel PermanentAddress { get; set; }
    public BranchMediumMessageModel BranchMedium { get; set; }
    public SessionMessageModel Session { get; set; }

    #endregion
  }
}