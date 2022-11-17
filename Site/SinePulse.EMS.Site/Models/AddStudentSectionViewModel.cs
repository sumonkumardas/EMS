using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class AddStudentSectionViewModel : BaseViewModel
  {
    public long InstituteId { get; set; }
    public long BranchId { get; set; }
    public long BranchMediumId { get; set; }
    public long StudentId { get; set; }
    public int RollNo { get; set; }
    public IEnumerable<Institute> Institutes { get; set; }
    public IEnumerable<BranchMessageModel> Branches { get; set; }
    public string MediumId { get; set; }
    public IEnumerable<MediumMessageModel> Mediums { get; set; }
    public string SessionId { get; set; }
    public IEnumerable<SessionMessageModel> Sessions { get; set; }
    public string SectionId { get; set; }
    public IEnumerable<SectionMessageModel> Sections { get; set; }
  }
}