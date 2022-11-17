using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Marks
{
  public class ShowMarkRequestHandler : IRequestHandler<ShowMarkRequestMessage, ValidatedData<ShowMarkResponseMessage>>
  {
    private readonly ShowMarkRequestMessageValidator _validator;
    private readonly IShowMarkUseCase _useCase;

    public ShowMarkRequestHandler(ShowMarkRequestMessageValidator validator, IShowMarkUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowMarkResponseMessage>> Handle(ShowMarkRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowMarkResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowMarkResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowMark(request);

      returnMessage = new ValidatedData<ShowMarkResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}