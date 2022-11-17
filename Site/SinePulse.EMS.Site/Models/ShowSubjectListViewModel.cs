using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class ShowSubjectListViewModel : BaseViewModel
  {
    public IEnumerable<SubjectMessageModel> Subjects { get; set; }
  }
}