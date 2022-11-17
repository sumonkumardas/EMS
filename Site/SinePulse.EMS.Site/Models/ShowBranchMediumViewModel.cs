using SinePulse.EMS.Domain.Academic;
using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.TermMessages;

namespace SinePulse.EMS.Site.Models
{
  public class ShowBranchMediumViewModel : BaseViewModel
  {
    public ShowBranchMediumViewModel()
    {
      BranchMedium = new BranchMedium();
    }
    public BranchMedium BranchMedium { get; set; }
    public long BranchMediumId { get; set; }
    public ICollection<SessionMessageModel> InstituteSessions { get; set; }
    public InstituteMessageModel Institute { get; set; }
    public TabEnum ActiveTab { get; set; }
    public long? CurrentSessionId { get; set; }
    public long SectionSessionId { get; set; }
    public long ExamTermSessionId { get; set; }
    [IgnoreMap]
    public TrialBalanceViewModel TrialBalanceViewModel { get; set; }
    public List<ShowTermListResponseMessage.Term> ExamTerms { get; set; }
  }
}