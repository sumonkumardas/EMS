using SinePulse.EMS.Messages.BankMessages;

namespace SinePulse.EMS.UseCases.Banks
{
  public interface IGetBankStatementInfoUseCase
  {
    GetBankStatementInfoResponseMessage.BankStatementInfo GetBankStatementInfo(
      GetBankStatementInfoRequestMessage message);
  }
}