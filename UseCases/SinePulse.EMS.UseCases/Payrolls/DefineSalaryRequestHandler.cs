using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.PayrollMessages;

namespace SinePulse.EMS.UseCases.Payrolls
{
  public class DefineSalaryRequestHandler : IRequestHandler<DefineSalaryRequestMessage, DefineSalaryResponseMessage>
  {
    private readonly DefineSalaryRequestMessageValidator _validator;
    private readonly IDefineSalaryUseCase _useCase;

    public DefineSalaryRequestHandler(DefineSalaryRequestMessageValidator validator, IDefineSalaryUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<DefineSalaryResponseMessage> Handle(DefineSalaryRequestMessage request,
      CancellationToken cancellationToken)
    {
      DefineSalaryResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new DefineSalaryResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.DefineSalary(request);

      returnMessage = new DefineSalaryResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}