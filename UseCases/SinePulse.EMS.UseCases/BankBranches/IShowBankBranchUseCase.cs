using SinePulse.EMS.Messages.BankBranchMessages;
using SinePulse.EMS.Messages.Model.Banks;

namespace SinePulse.EMS.UseCases.BankBranches
{
  public interface IShowBankBranchUseCase
  {
    BankBranchMessageModel GetBankBranch(ShowBankBranchRequestMessage message);
  }
}