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
  public class ShowTransactionUseCase : IShowTransactionUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _automapperConfig;

    public ShowTransactionUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Transaction, TransactionMessageModel>());
    }

    public TransactionMessageModel ShowTransaction(ShowTransactionRequestMessage message)
    {

      var includes = new[]
      {
        nameof(Transaction.TransactionEntries),nameof(Transaction.TransactionEntries)+"."+nameof(TransactionEntry.AccountHead),
        nameof(Transaction.TransactionEntries)+"."+nameof(TransactionEntry.AccountHead)+"."+nameof(TransactionEntry.AccountHead.AccountType)
      };
      var dbTransaction = _repository.GetByIdWithInclude<Transaction>(message.TransactionId,includes);
      var messageModelTransaction = new TransactionMessageModel();

      var mapper = _automapperConfig.CreateMapper();
      TransactionMessageModel model = mapper.Map(dbTransaction, messageModelTransaction);
      return model;
    }
  }
}