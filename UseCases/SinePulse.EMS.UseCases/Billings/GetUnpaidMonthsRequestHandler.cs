using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetUnpaidMonthsRequestHandler : IRequestHandler<GetUnpaidMonthsRequestMessage, GetUnpaidMonthsResponseMessage>
  {
    private readonly GetUnpaidMonthsRequestMessageValidator _validator;
    private readonly IGetUnpaidMonthsUseCase _useCase;

    public GetUnpaidMonthsRequestHandler(GetUnpaidMonthsRequestMessageValidator validator,
      IGetUnpaidMonthsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetUnpaidMonthsResponseMessage> Handle(GetUnpaidMonthsRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetUnpaidMonthsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetUnpaidMonthsResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var months = _useCase.GetUnpaidMonths(request);

      returnMessage = new GetUnpaidMonthsResponseMessage(validationResult, months);
      return Task.FromResult(returnMessage);
    }
  }
}