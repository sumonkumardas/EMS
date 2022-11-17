using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public interface IAddSalaryComponentTypeUseCase
  {
    SalaryComponentType AddSalaryComponentType(AddSalaryComponentTypeRequestMessage message);
  }
}
