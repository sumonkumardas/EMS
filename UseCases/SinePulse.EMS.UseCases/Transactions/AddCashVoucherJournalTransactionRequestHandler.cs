using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class AddCashVoucherJournalTransactionRequestHandler
    : IRequestHandler<AddCashVoucherJournalTransactionRequestMessage,
      ValidatedData<AddCashVoucherJournalTransactionResponseMessage>>
  {
    private readonly AddCashVoucherJournalTransactionRequestMessageValidator _validator;
    private readonly IAddCashVoucherJournalTransactionUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddCashVoucherJournalTransactionRequestHandler(AddCashVoucherJournalTransactionRequestMessageValidator validator,
      IAddCashVoucherJournalTransactionUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ValidatedData<AddCashVoucherJournalTransactionResponseMessage>> Handle(
      AddCashVoucherJournalTransactionRequestMessage request, CancellationToken cancellationToken)
    {
      ValidatedData<AddCashVoucherJournalTransactionResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<AddCashVoucherJournalTransactionResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.AddCashVoucherJournalTransaction(request);
      _dbContext.SaveChanges();

      returnMessage = new ValidatedData<AddCashVoucherJournalTransactionResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}