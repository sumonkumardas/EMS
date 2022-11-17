using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.GenerateSalarySheetMessages;

namespace SinePulse.EMS.UseCases.GenerateSalarySheets
{
  public class GetSalarySheetYearsRequestHandler : IRequestHandler<GetSalarySheetYearsRequestMessage, GetSalarySheetYearsResponseMessage>
  {
    private readonly GetSalarySheetYearsRequestMessageValidator _validator;
    private readonly IGetSalarySheetYearsUseCase _useCase;

    public GetSalarySheetYearsRequestHandler(GetSalarySheetYearsRequestMessageValidator validator,
      IGetSalarySheetYearsUseCase getSalarySheetYearsUseCase)
    {
      _validator = validator;
      _useCase = getSalarySheetYearsUseCase;
    }

    public Task<GetSalarySheetYearsResponseMessage> Handle(GetSalarySheetYearsRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetSalarySheetYearsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetSalarySheetYearsResponseMessage(validationResult, new List<int>());
        return Task.FromResult(returnMessage);
      }

      var years = _useCase.GetSalarySheetYears(request);

      returnMessage = new GetSalarySheetYearsResponseMessage(validationResult, years);
      return Task.FromResult(returnMessage);
    }
  }
}