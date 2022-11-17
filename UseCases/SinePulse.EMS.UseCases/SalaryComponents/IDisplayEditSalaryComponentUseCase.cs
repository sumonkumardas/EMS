using SinePulse.EMS.Messages.SalaryComponentMessages;
namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public interface IDisplayEditSalaryComponentUseCase
  {
    DisplayEditSalaryComponentResponseMessage DisplayEditSalaryComponent(DisplayEditSalaryComponentRequestMessage message);
  }
}
