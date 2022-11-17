using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class AddJournalTransactionRequestHandler
    : IRequestHandler<AddJournalTransactionRequestMessage,
      ValidatedData<AddJournalTransactionResponseMessage>>
  {
    private readonly AddJournalTransactionRequestMessageValidator _validator;
    private readonly IAddJournalTransactionUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddJournalTransactionRequestHandler(AddJournalTransactionRequestMessageValidator validator,
      IAddJournalTransactionUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ValidatedData<AddJournalTransactionResponseMessage>> Handle(
      AddJournalTransactionRequestMessage request, CancellationToken cancellationToken)
    {
      ValidatedData<AddJournalTransactionResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<AddJournalTransactionResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.AddJournalTransaction(request);
      _dbContext.SaveChanges();

      returnMessage = new ValidatedData<AddJournalTransactionResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}