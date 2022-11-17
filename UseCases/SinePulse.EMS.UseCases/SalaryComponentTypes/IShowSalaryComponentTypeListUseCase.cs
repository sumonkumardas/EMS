using SinePulse.EMS.Messages.SalaryComponentTypeMessage;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public interface IShowSalaryComponentTypeListUseCase
  {
    ShowSalaryComponentTypeListResponseMessage ShowSalaryComponentTypeList(ShowSalaryComponentTypeListRequestMessage message);
  }
}
