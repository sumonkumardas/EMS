using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class ImportSessionDataViewModel : BaseViewModel
  {
    public bool OnlySectionInfo { get; set; }
    public bool SectionInfoWithClassRoutine { get; set; }
    public bool OnlyExamTerm { get; set; }
    public bool ExamTermWithExamConfiguration { get; set; }
    public long PreviousSessionId { get; set; }
    public IList<Session> PreviousSessions { get; set; }
    public Session CurrentSession { get; set; }
    public long BranchMediumId { get; set; }

    public class Session
    {
      public long SessionId { get; set; }
      public string SessionText { get; set; }
    }
  }
}