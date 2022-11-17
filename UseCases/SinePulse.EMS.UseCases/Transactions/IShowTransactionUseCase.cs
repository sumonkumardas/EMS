using SinePulse.EMS.Messages.TransactionMessages;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Transactions;

namespace SinePulse.EMS.UseCases.Transactions
{
  public interface IShowTransactionUseCase
  {
    TransactionMessageModel ShowTransaction(ShowTransactionRequestMessage message);
  }
}