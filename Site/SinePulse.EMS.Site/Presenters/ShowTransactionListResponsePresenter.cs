using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowTransactionListResponsePresenter
  {
    public List<TransactionViewModel> Handle(ShowTransactionListResponseMessage message, List<TransactionViewModel> viewModel)
    {
      viewModel = message.TransactionList.Select(ToViewTransaction).ToList();
      return viewModel;
    }

    private TransactionViewModel ToViewTransaction(ShowTransactionListResponseMessage.Transaction transaction)
    {
      return new TransactionViewModel
      {
        Id = transaction.TransactionId,
        TransactionDate = transaction.TransactionDate,
        TransactionNo = transaction.TransactionNo,
        Description = transaction.Description
      };
    }
  }
}