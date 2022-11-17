using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Messages.Model.Transactions;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class ShowTransactionListUseCase : IShowTransactionListUseCase
  {
    private readonly IRepository _repository;

    public ShowTransactionListUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<ShowTransactionListResponseMessage.Transaction> ShowTransactionList(ShowTransactionListRequestMessage message)
    {

      var includes = new[]
      {
        nameof(Transaction.TransactionEntries),nameof(Transaction.TransactionEntries)+"."+nameof(TransactionEntry.AccountHead),
        nameof(Transaction.TransactionEntries)+"."+nameof(TransactionEntry.AccountHead)+"."+nameof(TransactionEntry.AccountHead.BranchMedium)
      };
      var dbTransactionList = _repository.Table<Transaction>().Where(x =>x.TransactionDate.DayOfYear >= message.StartDate.DayOfYear && x.TransactionDate.DayOfYear <= message.EndDate.DayOfYear && x.TransactionEntries.Any(y=>y.AccountHead.BranchMedium.Id == message.BranchMediumId));

      var list = new List<ShowTransactionListResponseMessage.Transaction>();

      foreach (var item in dbTransactionList)
      {
        var model = new ShowTransactionListResponseMessage.Transaction
        {
          TransactionDate = item.TransactionDate,
          Description = item.Description,
          TransactionId = item.Id,
          TransactionNo = item.TransactionNo
        };

        list.Add(model);
      }
      return list;
    }
  }
}