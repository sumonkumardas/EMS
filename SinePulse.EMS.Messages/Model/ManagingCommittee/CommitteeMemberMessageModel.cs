using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.ManagingCommittee
{
  public class CommitteeMemberMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string FullName { get; set; }
    public string NationalId { get; set; }
    public DateTime? DOB { get; set; }
    public string MobileNo { get; set; }
    public string EmailAddress { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string SpouseName { get; set; }
    public string ImageUrl { get; set; }
    public StatusEnum Status { get; set; }

    #endregion

    #region Navigation Properties

    public InstituteMessageModel Institute { get; set; }
    public DesignationMessageModel Designation { get; set; }
    public AddressMessageModel PresentAddress { get; set; }
    public AddressMessageModel PermanentAddress { get; set; }

    #endregion
  }
}