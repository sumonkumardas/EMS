using SinePulse.EMS.Messages.SalaryComponentTypeMessage;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public interface IEditSalaryComponentTypeUseCase
  {
    EditSalaryComponentTypeResponseMessage EditSalaryComponentType(EditSalaryComponentTypeRequestMessage requestMessage);
  }
}
