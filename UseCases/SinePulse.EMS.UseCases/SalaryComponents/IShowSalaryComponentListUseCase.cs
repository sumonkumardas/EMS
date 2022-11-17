using SinePulse.EMS.Messages.SalaryComponentMessages;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public interface IShowSalaryComponentListUseCase
  {
    ShowSalaryComponentListResponseMessage ShowSalaryComponentList(ShowSalaryComponentListRequestMessage message);
  }
}
