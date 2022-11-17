using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class GetBankStatementRequestHandler: IRequestHandler<GetBankStatementRequestMessage, GetBankStatementResponseMessage>
  {
    private readonly GetBankStatementRequestMessageValidator _validator;
    private readonly IGetBankStatementUseCase _useCase;

    public GetBankStatementRequestHandler(GetBankStatementRequestMessageValidator validator,
      IGetBankStatementUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetBankStatementResponseMessage> Handle(GetBankStatementRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetBankStatementResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetBankStatementResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var bankStatements = _useCase.GetBankStatement(request);

      returnMessage = new GetBankStatementResponseMessage(validationResult, bankStatements);
      return Task.FromResult(returnMessage);
    } 
  }
}