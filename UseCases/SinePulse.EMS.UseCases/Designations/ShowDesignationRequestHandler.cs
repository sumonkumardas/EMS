using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.DesignationMessages;

namespace SinePulse.EMS.UseCases.Designations
{
  public class ShowDesignationRequestHandler : IRequestHandler<ShowDesignationRequestMessage, ShowDesignationResponseMessage>
  {
    private readonly ShowDesignationRequestMessageValidator _validator;
    private readonly IShowDesignationUseCase _useCase;

    public ShowDesignationRequestHandler(ShowDesignationRequestMessageValidator validator,
      IShowDesignationUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowDesignationResponseMessage> Handle(ShowDesignationRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowDesignationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowDesignationResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var designation = _useCase.ShowDesignation(request);

      returnMessage = new ShowDesignationResponseMessage(validationResult, designation);
      return Task.FromResult(returnMessage);
    }
  }
}