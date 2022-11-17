using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class ResultGradeListViewModel : BaseViewModel
  {
    public long BranchMediumId { get; set; }
    public List<ResultGradeViewModel> ResultGrades = new List<ResultGradeViewModel>();
  }
}
