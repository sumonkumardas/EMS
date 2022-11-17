using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.JobTypeMessages;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class ShowJobTypeRequestHandler : IRequestHandler<ShowJobTypeRequestMessage, ShowJobTypeResponseMessage>
  {
    private readonly ShowJobTypeRequestMessageValidator _validator;
    private readonly IShowJobTypeUseCase _useCase;

    public ShowJobTypeRequestHandler(ShowJobTypeRequestMessageValidator validator,
      IShowJobTypeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowJobTypeResponseMessage> Handle(ShowJobTypeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowJobTypeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowJobTypeResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var jobType = _useCase.ShowJobType(request);

      returnMessage = new ShowJobTypeResponseMessage(validationResult, jobType);
      return Task.FromResult(returnMessage);
    }
  }
}