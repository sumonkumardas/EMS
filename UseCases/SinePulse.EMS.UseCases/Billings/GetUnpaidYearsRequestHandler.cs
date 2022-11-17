using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BillingMessages;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetUnpaidYearsRequestHandler : IRequestHandler<GetUnpaidYearsRequestMessage, GetUnpaidYearsResponseMessage>
  {
    private readonly GetUnpaidYearsRequestMessageValidator _validator;
    private readonly IGetUnpaidYearsUseCase _useCase;

    public GetUnpaidYearsRequestHandler(GetUnpaidYearsRequestMessageValidator validator,
      IGetUnpaidYearsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetUnpaidYearsResponseMessage> Handle(GetUnpaidYearsRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetUnpaidYearsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetUnpaidYearsResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var years = _useCase.GetUnpaidYears(request);

      returnMessage = new GetUnpaidYearsResponseMessage(validationResult, years);
      return Task.FromResult(returnMessage);
    }
  }
}