using MediatR;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.Messages.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class ShowSalaryComponentTypeListRequestHandler : IRequestHandler<ShowSalaryComponentTypeListRequestMessage, ValidatedData<ShowSalaryComponentTypeListResponseMessage>>
  {
    private readonly ShowSalaryComponentTypeListRequestMessageValidator _validator;
    private readonly IShowSalaryComponentTypeListUseCase _useCase;

    public ShowSalaryComponentTypeListRequestHandler(ShowSalaryComponentTypeListRequestMessageValidator validator, IShowSalaryComponentTypeListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowSalaryComponentTypeListResponseMessage>> Handle(ShowSalaryComponentTypeListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowSalaryComponentTypeListResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowSalaryComponentTypeListResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowSalaryComponentTypeList(request);

      returnMessage = new ValidatedData<ShowSalaryComponentTypeListResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}
