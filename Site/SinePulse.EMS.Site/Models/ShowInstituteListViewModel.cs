using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class ShowInstituteListViewModel : BaseViewModel
  {
    public ShowInstituteListViewModel()
    {
      InstituteList = new List<Institute>();
    }

    public List<Institute> InstituteList { get; set; }
    public List<InstituteViewModel> InstituteViewModelList { get; set; }
  }
}