using SinePulse.EMS.Messages.BankBranchMessages;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public interface IAddBankBranchUseCase
  {
    BankBranchMessageModel AddBankBranch(AddBankBranchRequestMessage message);
  }
}