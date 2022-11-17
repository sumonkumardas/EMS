using SinePulse.EMS.Messages.SalaryComponentMessages;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public interface IEditSalaryComponentUseCase
  {
    void EditSalaryComponent(EditSalaryComponentRequestMessage message);
  }
}
