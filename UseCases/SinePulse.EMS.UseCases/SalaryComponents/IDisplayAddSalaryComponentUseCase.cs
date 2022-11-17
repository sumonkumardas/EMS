using SinePulse.EMS.Messages.SalaryComponentMessages;
namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public interface IDisplayAddSalaryComponentUseCase
  {
    DisplayAddSalaryComponentResponseMessage DisplayAddSalaryComponent(DisplayAddSalaryComponentRequestMessage message);
  }
}
