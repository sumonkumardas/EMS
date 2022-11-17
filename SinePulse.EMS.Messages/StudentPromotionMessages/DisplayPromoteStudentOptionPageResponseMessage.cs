using System.Collections.Generic;

namespace SinePulse.EMS.Messages.StudentPromotionMessages
{
  public class DisplayPromoteStudentOptionPageResponseMessage
  {
    public ICollection<Session> Sessions { get; set; }
    public ICollection<Class> Classes { get; set; }

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