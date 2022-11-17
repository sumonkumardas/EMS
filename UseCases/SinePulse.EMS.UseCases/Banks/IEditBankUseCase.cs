using SinePulse.EMS.Messages.BankMessages;

namespace SinePulse.EMS.UseCases.Banks
{
  public interface IEditBankUseCase
  {
    long EditBank(EditBankRequestMessage message);
  }
}