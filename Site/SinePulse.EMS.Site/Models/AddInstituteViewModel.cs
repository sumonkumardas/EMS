using SinePulse.EMS.Domain.Academic;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class AddInstituteViewModel : BaseViewModel
  {
    public AddInstituteViewModel()
    {
      BrachList = new List<Branch>();
    }
    public string OrganisationName { get; set; }
    public string ShortName { get; set; }
    public string Slogan { get; set; }
    public string Domain { get; set; }
    public string Email { get; set; }
    public byte[] Favicon { get; set; }
    public byte[] Logo { get; set; }
    public byte[] Banner { get; set; }
    public string Description { get; set; }
    public string WhyChooseInstitue { get; set; }
    public string FacebookPageURL { get; set; }
    public List<Branch> BrachList { get; set; }
  }
}
