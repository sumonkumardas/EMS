using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class PromoteStudentOptionViewModel : BaseViewModel
  {
    public long SectionId { get; set; }

    public IList<Session> Sessions { get; set; }
    public IList<Class> Classes { get; set; }
    public long SessionId { get; set; }
    public long ClassId { get; set; }


    public class Session
    {
      public long SessionId { get; set; }
      public string SessionName { get; set; }
    }

    public class Class
    {
      public long ClassId { get; set; }
      public string ClassName { get; set; }
    }
  }
}