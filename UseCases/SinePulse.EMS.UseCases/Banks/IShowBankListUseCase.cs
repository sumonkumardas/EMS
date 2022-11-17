using System.Collections.Generic;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.UseCases.Banks
{
  public interface IShowBankListUseCase
  {
    List<ShowBankListResponseMessage.Bank> GetBankList(ShowBankListRequestMessage message);
  }
}