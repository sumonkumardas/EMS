using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Messages.SalaryComponentTypeMessage
{
  public class EditSalaryComponentTypeResponseMessage
  {
    public long SalaryComponentTypeId { get; }

    public EditSalaryComponentTypeResponseMessage(long salaryComponentTypeId)
    {
      SalaryComponentTypeId = salaryComponentTypeId;
    }
  }
}
