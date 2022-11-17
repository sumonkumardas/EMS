using SinePulse.EMS.Messages.SalaryComponentTypeMessage;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public interface IDisplayEditSalaryComponentTypeUseCase
  {
    DisplayEditSalaryComponentTypeResponseMessage ShowSalaryComponentType(DisplayEditSalaryComponentTypeRequestMessage message);
  }
}
