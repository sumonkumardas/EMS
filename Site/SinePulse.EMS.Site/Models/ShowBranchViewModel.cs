using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class ShowBranchViewModel : BaseViewModel
  {
    public ShowBranchViewModel()
    {
      Branch = new Branch();
    }

    public Branch Branch { get; set; }
    public ICollection<BranchMediumMessageModel> BranchMediums { get; set; }
    public ICollection<Session> InstituteSessions { get; set; }
    public TabEnum ActiveTab { get; set; }
  }
}