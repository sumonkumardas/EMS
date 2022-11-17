using SinePulse.EMS.Messages.Model.Enums;

namespace SinePulse.EMS.Site.Models
{
  public class AddStudentContactPersonViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
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
    public long StudentId { get; set; }
  }
}