using System.Collections.Generic;

namespace SinePulse.EMS.Messages.SalaryComponentMessages
{
  public class DisplayAddSalaryComponentResponseMessage
  {
    public ICollection<SalaryComponentType> ComponentTypes { get;}
    public DisplayAddSalaryComponentResponseMessage(ICollection<SalaryComponentType> componentTypes)
    {
      ComponentTypes = componentTypes;
    }
    
    public class SalaryComponentType
    {
      public long ComponentTypeId { get; set; }
      public string ComponentTypeName { get; set; }
    }
  }
}
