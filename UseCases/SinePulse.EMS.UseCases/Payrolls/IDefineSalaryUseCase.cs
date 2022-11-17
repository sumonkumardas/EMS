using SinePulse.EMS.Messages.PayrollMessages;

namespace SinePulse.EMS.UseCases.Payrolls
{
  public interface IDefineSalaryUseCase
  {
    void DefineSalary(DefineSalaryRequestMessage message);
  }
}