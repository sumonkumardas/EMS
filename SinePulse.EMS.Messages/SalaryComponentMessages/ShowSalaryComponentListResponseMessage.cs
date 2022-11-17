using System.Collections.Generic;

namespace SinePulse.EMS.Messages.SalaryComponentMessages
{
  public class ShowSalaryComponentListResponseMessage
  {
    public List<SalaryComponent> SalaryComponentData { get; }

    public ShowSalaryComponentListResponseMessage(List<SalaryComponent> salaryComponent)
    {
      SalaryComponentData = salaryComponent;
    }

    public class SalaryComponent
    {
      public long SalaryComponentId { get; set; }
      public string ComponentName { get; set; }
      public SalaryComponentType ComponentType { get; set; }
    }

    public class SalaryComponentType
    {
      public long SalaryComponentTypeId { get; set; }
      public string ComponentType { get; set; }
    }
  }
}
