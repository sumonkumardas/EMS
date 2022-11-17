using System.Collections.Generic;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public interface IGetBankStatementUseCase
  {
    List<GetBankStatementResponseMessage.BankStatement> GetBankStatement(GetBankStatementRequestMessage message);
  }
}