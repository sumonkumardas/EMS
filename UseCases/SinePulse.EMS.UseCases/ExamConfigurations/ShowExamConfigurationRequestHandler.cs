using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class ShowExamConfigurationRequestHandler : IRequestHandler<ShowExamConfigurationRequestMessage, ValidatedData<ShowExamConfigurationResponseMessage>>
  {
    private readonly ShowExamConfigurationRequestMessageValidator _validator;
    private readonly IShowExamConfigurationUseCase _useCase;

    public ShowExamConfigurationRequestHandler(ShowExamConfigurationRequestMessageValidator validator, IShowExamConfigurationUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowExamConfigurationResponseMessage>> Handle(ShowExamConfigurationRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowExamConfigurationResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowExamConfigurationResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowExamConfiguration(request);

      returnMessage = new ValidatedData<ShowExamConfigurationResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}