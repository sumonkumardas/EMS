using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Site.Models
{
  public class AddCommitteeMemberViewModel : BaseViewModel
  {
    public string FullName { get; set; }
    public DateTime? DOB { get; set; }
    public string MobileNo { get; set; }
    public string EmailAddress { get; set; }
    public string NationalId { get; set; }
    public StatusEnum Status { get; set; }
    public new long InstituteId { get; set; }
    public long DesignationId { get; set; }
    public IEnumerable<DesignationMessageModel> Designations { get; set; }
  }
}