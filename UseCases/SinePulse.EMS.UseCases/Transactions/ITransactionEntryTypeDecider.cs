using System.Threading.Tasks;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.UseCases.Transactions
{
  public interface ITransactionEntryTypeDecider
  {
    Task<TransactionEntryType> GetTransactionEntryTypeFromAccountType(decimal amount, long accountTypeId);
  }
}