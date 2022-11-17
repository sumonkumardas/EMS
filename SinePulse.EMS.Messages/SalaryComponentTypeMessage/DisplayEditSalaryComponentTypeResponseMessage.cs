using SinePulse.EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Messages.SalaryComponentTypeMessage
{
  public class DisplayEditSalaryComponentTypeResponseMessage
  {
    public SalaryComponentType ComponentType { get; set; }

    public DisplayEditSalaryComponentTypeResponseMessage(SalaryComponentType componentType)
    {
      ComponentType = componentType;
    }
    public class SalaryComponentType
    {
      public long Id { get; set; }
      public string ComponentTypeName { get; set; }
      public StatusEnum Status { get; set; }
    }
  }
}
