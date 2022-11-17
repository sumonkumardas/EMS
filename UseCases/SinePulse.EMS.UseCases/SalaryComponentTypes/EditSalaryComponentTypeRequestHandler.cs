using MediatR;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.Messages.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class EditSalaryComponentTypeRequestHandler : IRequestHandler<EditSalaryComponentTypeRequestMessage, ValidatedData<EditSalaryComponentTypeResponseMessage>>
  {
    private readonly EditSalaryComponentTypeRequestMessageValidator _validator;
    private readonly IEditSalaryComponentTypeUseCase _useCase;


    public EditSalaryComponentTypeRequestHandler(EditSalaryComponentTypeRequestMessageValidator validator,
      IEditSalaryComponentTypeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<EditSalaryComponentTypeResponseMessage>> Handle(EditSalaryComponentTypeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<EditSalaryComponentTypeResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<EditSalaryComponentTypeResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.EditSalaryComponentType(request);

      returnMessage = new ValidatedData<EditSalaryComponentTypeResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}
