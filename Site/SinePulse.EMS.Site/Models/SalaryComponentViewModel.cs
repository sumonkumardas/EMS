using SinePulse.EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Models
{
  public class SalaryComponentViewModel : BaseViewModel
  {
    public long SalaryComponentId { get; set; }
    public string ComponentName { get; set; }
    public SalaryComponentType componentTypeData { get; set; }
    public class SalaryComponentType
    {
      public long SalaryComponentTypeId { get; set; }
      public string ComponentType { get; set; }
    }
  }
}
