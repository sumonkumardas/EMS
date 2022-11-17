using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class ShowExamTypeRequestHandler : IRequestHandler<ShowExamTypeRequestMessage, ValidatedData<ShowExamTypeResponseMessage>>
  {
    private readonly ShowExamTypeRequestMessageValidator _validator;
    private readonly IShowExamTypeUseCase _useCase;

    public ShowExamTypeRequestHandler(ShowExamTypeRequestMessageValidator validator, IShowExamTypeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowExamTypeResponseMessage>> Handle(ShowExamTypeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowExamTypeResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowExamTypeResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowExamType(request);

      returnMessage = new ValidatedData<ShowExamTypeResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}