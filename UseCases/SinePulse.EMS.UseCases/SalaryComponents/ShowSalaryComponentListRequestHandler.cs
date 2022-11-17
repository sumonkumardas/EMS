using MediatR;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.UseCases.SalaryComponentTypes;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class ShowSalaryComponentListRequestHandler : IRequestHandler<ShowSalaryComponentListRequestMessage, ValidatedData<ShowSalaryComponentListResponseMessage>>
  {
    private readonly ShowSalaryComponentListRequestMessageValidator _validator;
    private readonly IShowSalaryComponentListUseCase _useCase;

    public ShowSalaryComponentListRequestHandler(ShowSalaryComponentListRequestMessageValidator validator, IShowSalaryComponentListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowSalaryComponentListResponseMessage>> Handle(ShowSalaryComponentListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowSalaryComponentListResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowSalaryComponentListResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowSalaryComponentList(request);

      returnMessage = new ValidatedData<ShowSalaryComponentListResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}
