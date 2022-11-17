using System.Linq;
using SinePulse.EMS.Domain.PaymentGateway;
using SinePulse.EMS.Messages.OnlinePaymentMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class DeleteFailedTransactionUseCase : IDeleteFailedTransactionUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public DeleteFailedTransactionUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void DeleteFailedTransaction(DeleteFailedTransactionRequestMessage message)
    {
      var transaction = _repository.Table<SslPaymentGateway>()
        .FirstOrDefault(s => s.TransactionId == message.TransactionId);

      if (transaction != null)
      {
        _repository.Delete(transaction);
        _dbContext.SaveChanges();
      }
    }
  }
}