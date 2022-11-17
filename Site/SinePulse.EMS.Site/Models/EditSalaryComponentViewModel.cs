using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class EditSalaryComponentViewModel :BaseViewModel
  {
    public long SalaryComponentId { get; set; }
    public string ComponentName { get; set; }
    public long SalaryComponentTypeId { get; set; }
    public ICollection<SalaryComponentType> SalaryComponentTypes { get; set; }
    public class SalaryComponentType
    {
      public long ComponentTypeId { get; set; }
      public string ComponentTypeName { get; set; }
    }
  }
}
