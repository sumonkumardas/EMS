using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class ShowClassesListViewModel : BaseViewModel
  {
    public IEnumerable<ClassMessageModel> Classes { get; set; }
  }
}