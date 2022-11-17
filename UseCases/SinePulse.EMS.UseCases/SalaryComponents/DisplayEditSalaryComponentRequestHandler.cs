using MediatR;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Messages.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class DisplayEditSalaryComponentRequestHandler : IRequestHandler<DisplayEditSalaryComponentRequestMessage, ValidatedData<DisplayEditSalaryComponentResponseMessage>>
  {
    private readonly DisplayEditSalaryComponentRequestMessageValidator _validator;
    private readonly IDisplayEditSalaryComponentUseCase _useCase;

    public DisplayEditSalaryComponentRequestHandler(DisplayEditSalaryComponentRequestMessageValidator validator, IDisplayEditSalaryComponentUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayEditSalaryComponentResponseMessage>> Handle(DisplayEditSalaryComponentRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayEditSalaryComponentResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayEditSalaryComponentResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.DisplayEditSalaryComponent(request);

      returnMessage = new ValidatedData<DisplayEditSalaryComponentResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}
