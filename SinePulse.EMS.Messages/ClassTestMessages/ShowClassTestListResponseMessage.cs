using System;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.ClassTestMessages
{
  public class ShowClassTestListResponseMessage
  {
    public List<ClassTest> ClassTestData { get; }

    public ShowClassTestListResponseMessage(List<ClassTest> classTest)
    {
      ClassTestData = classTest;
    }

    public class ClassTest
    {
      public long TestId { get; set; }
      
      public string TestName { get; set; }

      public DateTime StartTimestamp { get; set; }

      public DateTime EndTimestamp { get; set; }
    }
  }
}