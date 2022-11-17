using System.Linq;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Persistence.Repositories;
using static SinePulse.EMS.Messages.TransactionMessages.AddGenericJournalTransactionRequestMessage;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class AddCashVoucherJournalTransactionUseCase : IAddCashVoucherJournalTransactionUseCase
  {
    private readonly IRepository _repository;
    private readonly IAddGenericJournalTransactionUseCase _addGenericJournalTransactionUseCase;

    public AddCashVoucherJournalTransactionUseCase(
      IAddGenericJournalTransactionUseCase addGenericJournalTransactionUseCase, IRepository repository)
    {
      _addGenericJournalTransactionUseCase = addGenericJournalTransactionUseCase;
      _repository = repository;
    }

    public AddCashVoucherJournalTransactionResponseMessage AddCashVoucherJournalTransaction(
      AddCashVoucherJournalTransactionRequestMessage requestMessage)
    {
      var addGenericJournalTransactionRequestMessage = GetGenericJournalTransactionRequestMessage(requestMessage);
      var responseMessage =
        _addGenericJournalTransactionUseCase.AddGenericJournalTransaction(
          addGenericJournalTransactionRequestMessage);

      return new AddCashVoucherJournalTransactionResponseMessage
      {
        TransactionId = responseMessage.TransactionId,
        TransactionNo = responseMessage.TransactionNo
      };
    }
    public AddGenericJournalTransactionRequestMessage GetGenericJournalTransactionRequestMessage(AddCashVoucherJournalTransactionRequestMessage requestMessage)
    {
      var totalAmount = requestMessage.TransactionEntries.Sum(x => x.Amount);
      var transactionEntries = requestMessage.TransactionEntries
        .Select(x => GetTransactionEntry(x.AccountHeadId, x.Amount, !requestMessage.IsDebitTransaction)).ToList();

      var cashVoucherAccountHeadId = GetCashVoucherAccountHeadId(requestMessage);
      var cashVoucherTransactionEntry =
        GetTransactionEntry(cashVoucherAccountHeadId, totalAmount, requestMessage.IsDebitTransaction);
      transactionEntries.Add(cashVoucherTransactionEntry);

      var addGenericJournalTransactionRequestMessage = new AddGenericJournalTransactionRequestMessage
      {
        TransactionEntries = transactionEntries,
        BranchMediumId = requestMessage.BranchMediumId,
        CurrentUserName = requestMessage.CurrentUserName,
        Description = requestMessage.Description,
        SessionId = requestMessage.SessionId,
        TransactionDate = requestMessage.TransactionDate
      };

      return addGenericJournalTransactionRequestMessage;
    }
    private TransactionEntry GetTransactionEntry(long accountHeadId, decimal amount, bool isDebit)
    {
      var transactionEntry = new TransactionEntry
      {
        AccountHeadId = accountHeadId,
        CreditAmount = isDebit ? 0 : amount,
        DebitAmount = isDebit ? amount : 0
      };
      return transactionEntry;
    }

    private long GetCashVoucherAccountHeadId(AddCashVoucherJournalTransactionRequestMessage requestMessage)
    {
      var cashVoucherConfiguration = _repository.Filter<AutoPostingConfiguration>(
        x => x.VoucherType == VoucherTypeEnum.CashVoucher).First();
      var cashVoucherAccountCode = cashVoucherConfiguration.AccountCode;

      var branchMediumAccountHead = _repository.Filter<BranchMediumAccountHead>(
        x => x.Session.Id == requestMessage.SessionId &&
             x.AccountCode == cashVoucherAccountCode).First();

      return branchMediumAccountHead.Id;
    }
  }
}