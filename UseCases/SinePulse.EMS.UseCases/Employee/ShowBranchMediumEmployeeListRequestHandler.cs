using FluentValidation;
using MediatR;
using SinePulse.EMS.Messages.EmployeeMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowBranchMediumEmployeeListRequestHandler : IRequestHandler<ShowBranchMediumEmployeeListRequestMessage, ShowEmployeeListResponseMessage>
  {
    private readonly ShowBranchMediumEmployeeListRequestMessageValidator _validator;
    private readonly IShowBranchMediumEmployeeListUseCase _useCase;

    public ShowBranchMediumEmployeeListRequestHandler(ShowBranchMediumEmployeeListRequestMessageValidator validator,
      IShowBranchMediumEmployeeListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowEmployeeListResponseMessage> Handle(ShowBranchMediumEmployeeListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowEmployeeListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowEmployeeListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var employeeList = _useCase.ShowBranchMediumEmployeeList(request);

      returnMessage = new ShowEmployeeListResponseMessage(validationResult, employeeList);
      return Task.FromResult(returnMessage);
    }
  }
}
