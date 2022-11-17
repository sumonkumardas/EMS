using System.Threading.Tasks;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.UseCases.Transactions
{
  public interface ITransactionEntryValueDecider
  {
    Task<decimal> GetTransactionEntryValueFromAccountType(decimal amount, long accountTypeId,
      TransactionEntryType transactionEntryType);
  }
}