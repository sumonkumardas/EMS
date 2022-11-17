using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.IncomeStatementMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.IncomeStatements
{
  public class ShowIncomeStatementRequestHandler
    : IRequestHandler<ShowIncomeStatementRequestMessage, ValidatedData<ShowIncomeStatementResponseMessage>>
  {
    private readonly ShowIncomeStatementRequestMessageValidator _validator;
    private readonly IShowIncomeStatementUseCase _useCase;


    public ShowIncomeStatementRequestHandler(ShowIncomeStatementRequestMessageValidator validator,
      IShowIncomeStatementUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowIncomeStatementResponseMessage>> Handle(ShowIncomeStatementRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowIncomeStatementResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowIncomeStatementResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var data = _useCase.ShowIncomeStatement(request);

      returnMessage = new ValidatedData<ShowIncomeStatementResponseMessage>(validationResult, data);
      return Task.FromResult(returnMessage);
    }
  }
}