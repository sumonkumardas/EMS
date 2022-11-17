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
  public class ShowTransactionListRequestHandler : IRequestHandler<ShowTransactionListRequestMessage, ShowTransactionListResponseMessage>
  {
    private readonly ShowTransactionListRequestMessageValidator _validator;
    private readonly IShowTransactionListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowTransactionListRequestHandler(ShowTransactionListRequestMessageValidator validator,
      IShowTransactionListUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowTransactionListResponseMessage> Handle(ShowTransactionListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowTransactionListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowTransactionListResponseMessage(validationResult,new List<ShowTransactionListResponseMessage.Transaction>());
        return Task.FromResult(returnMessage);
      }

      var model = _useCase.ShowTransactionList(request);

      returnMessage = new ShowTransactionListResponseMessage(validationResult,model);
      return Task.FromResult(returnMessage);
    }
  }
}