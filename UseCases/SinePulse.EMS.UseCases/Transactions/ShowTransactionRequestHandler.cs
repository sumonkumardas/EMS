using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Messages.Model.Transactions;

namespace SinePulse.EMS.UseCases.Transactions
{
  public class ShowTransactionRequestHandler : IRequestHandler<ShowTransactionRequestMessage, ShowTransactionResponseMessage>
  {
    private readonly ShowTransactionRequestMessageValidator _validator;
    private readonly IShowTransactionUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowTransactionRequestHandler(ShowTransactionRequestMessageValidator validator,
      IShowTransactionUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowTransactionResponseMessage> Handle(ShowTransactionRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowTransactionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowTransactionResponseMessage(validationResult,new TransactionMessageModel());
        return Task.FromResult(returnMessage);
      }

      var model = _useCase.ShowTransaction(request);

      returnMessage = new ShowTransactionResponseMessage(validationResult,model);
      return Task.FromResult(returnMessage);
    }
  }
}