using SinePulse.EMS.Domain.Enums;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.SalaryComponentTypeMessage
{
  public class ShowSalaryComponentTypeListResponseMessage
  {
    public List<SalaryComponentType> SalaryComponentTypeData { get; }

    public ShowSalaryComponentTypeListResponseMessage(List<SalaryComponentType> salaryComponentType)
    {
      SalaryComponentTypeData = salaryComponentType;
    }

    public class SalaryComponentType
    {
      public long Id { get; set; }
      public string ComponentType { get; set; }
      public StatusEnum Status { get; set; }
    }
  }
}
