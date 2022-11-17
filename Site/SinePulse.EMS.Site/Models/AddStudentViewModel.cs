using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class AddStudentViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public string FullName { get; set; }
    public RelationTypeEnum Guardian { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public GroupEnum Group { get; set; }
    public long ClassId { get; set; }
    public string SectionId { get; set; }
    public IEnumerable<SectionMessageModel> Sections { get; set; }
    public List<FilteredGroup> Groups { get; set; }
    public int RollNo { get; set; }

    public class FilteredGroup
    {
      public long GroupId { get; set; }
      public string GroupName { get; set; }
    }
  }
}