using System.Collections.Generic;

namespace SinePulse.EMS.Messages.SalaryComponentMessages
{
  public class DisplayEditSalaryComponentResponseMessage
  {
    public ICollection<SalaryComponentType> ComponentTypes { get; }
    public DisplayEditSalaryComponentResponseMessage(ICollection<SalaryComponentType> componentTypes)
    {
      ComponentTypes = componentTypes;
    }
    public long SalaryComponentId { get; set; }
    public string ComponentName { get; set; }

    public long ComponentTypeId { get; set; }
    public class SalaryComponentType
    {
      public long ComponentTypeId { get; set; }
      public string ComponentTypeName { get; set; }
    }
  }
}
