using SinePulse.EMS.Messages.IncomeStatementMessages;

namespace SinePulse.EMS.UseCases.IncomeStatements
{
  public interface IShowIncomeStatementUseCase
  {
    ShowIncomeStatementResponseMessage ShowIncomeStatement(ShowIncomeStatementRequestMessage requestMessage);
  }
}