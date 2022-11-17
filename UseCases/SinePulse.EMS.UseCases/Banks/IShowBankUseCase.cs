using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.UseCases.Banks
{
  public interface IShowBankUseCase
  {
    BankMessageModel GetBank(ShowBankRequestMessage message);
  }
}