using System;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.TermMessages
{
  public class ShowTermListResponseMessage
  {
    public List<Term> TermDataList { get; }

    public ShowTermListResponseMessage(List<Term> termDataList)
    {
      TermDataList = termDataList;
    }

    public class Term
    {
      public long Id { get; set; }

      public string TermName { get; set; }

      public DateTime StartDate { get; set; }

      public DateTime EndDate { get; set; }
      public long SessionId { get; set; }
      
      public string SessionName { get; set; }
    }
  }
}