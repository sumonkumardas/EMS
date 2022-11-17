using System;
using System.Linq;
using SinePulse.EMS.Domain.PaymentGateway;
using SinePulse.EMS.Messages.OnlinePaymentMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class UpdateTransactionInfoUseCase : IUpdateTransactionInfoUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public UpdateTransactionInfoUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void UpdateTransactionInfo(UpdateTransactionInfoRequestMessage message)
    {
      var transactionInfo = _repository.Table<SslPaymentGateway>()
        .FirstOrDefault(s => s.TransactionId == message.TransactionId);

      if (transactionInfo != null)
      {
        transactionInfo.PaymentDate = message.PaymentDate;
        transactionInfo.CardType = message.CardType;
        transactionInfo.StoreAmount = message.StoreAmount;
        transactionInfo.ValidationId = message.ValidationId;
        transactionInfo.AuditFields.LastUpdatedBy = message.CurrentUserName;
        transactionInfo.AuditFields.LastUpdatedDateTime = DateTime.Now;
      }

      _dbContext.SaveChanges();
    }
  }
}