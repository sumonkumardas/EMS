using MediatR;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Messages.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class DisplayAddSalaryComponentRequestHandler : IRequestHandler<DisplayAddSalaryComponentRequestMessage, ValidatedData<DisplayAddSalaryComponentResponseMessage>>
  {
    private readonly DisplayAddSalaryComponentRequestMessageValidator _validator;
    private readonly IDisplayAddSalaryComponentUseCase _useCase;

    public DisplayAddSalaryComponentRequestHandler(DisplayAddSalaryComponentRequestMessageValidator validator, IDisplayAddSalaryComponentUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddSalaryComponentResponseMessage>> Handle(DisplayAddSalaryComponentRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddSalaryComponentResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddSalaryComponentResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.DisplayAddSalaryComponent(request);

      returnMessage = new ValidatedData<DisplayAddSalaryComponentResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}
