using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalaryComponentMessages;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public interface IAddSalaryComponentUseCase
  {
    SalaryComponent AddSalaryComponent(AddSalaryComponentRequestMessage message);
  }
}
