using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class ShowBranchMediumSectionListViewModel : BaseViewModel
  {
    public IEnumerable<SectionMessageModel> Sections { get; set; }
  }
}