using System.Collections.Generic;

namespace SinePulse.EMS.Site.Models
{
  public class AddSalaryComponentViewModel : BaseViewModel
  {
    public string ComponentName { get; set; }
    public long SalaryComponentTypeId { get; set; }
    public ICollection<SalaryComponentType>  SalaryComponentTypes { get; set; }
    public class SalaryComponentType
    {
      public long ComponentTypeId { get; set; }
      public string ComponentTypeName { get; set; }
    }
  }
}
