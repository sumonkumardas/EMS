using MediatR;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.Messages.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class DisplayEditSalaryComponentTypeRequestHandler : IRequestHandler<DisplayEditSalaryComponentTypeRequestMessage, ValidatedData<DisplayEditSalaryComponentTypeResponseMessage>>
  {
    private readonly DisplayEditSalaryComponentTypeRequestMessageValidator _validator;
    private readonly IDisplayEditSalaryComponentTypeUseCase _useCase;

    public DisplayEditSalaryComponentTypeRequestHandler(DisplayEditSalaryComponentTypeRequestMessageValidator validator, IDisplayEditSalaryComponentTypeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayEditSalaryComponentTypeResponseMessage>> Handle(DisplayEditSalaryComponentTypeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayEditSalaryComponentTypeResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayEditSalaryComponentTypeResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowSalaryComponentType(request);

      returnMessage = new ValidatedData<DisplayEditSalaryComponentTypeResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}
