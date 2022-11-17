using SinePulse.EMS.Messages.BankBranchMessages;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public interface IEditBankBranchUseCase
  {
    void EditBankBranch(EditBankBranchRequestMessage message);
  }
}