using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassMessages;

namespace SinePulse.EMS.UseCases.Classes
{
  public class ShowClassesListRequestHandler : IRequestHandler<ShowClassesListRequestMessage, ShowClassesListResponseMessage>
  {
    private readonly ShowClassesListRequestMessageValidator _validator;
    private readonly IShowClassesListUseCase _useCase;

    public ShowClassesListRequestHandler(ShowClassesListRequestMessageValidator validator, IShowClassesListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowClassesListResponseMessage> Handle(ShowClassesListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowClassesListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowClassesListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var classes = _useCase.GetClasses(request);

      returnMessage = new ShowClassesListResponseMessage(validationResult, classes);
      return Task.FromResult(returnMessage);
    }
  }
}