using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class DisplayAddExamTypePageRequestHandler
    : IRequestHandler<DisplayAddExamConfigurationPageRequestMessage, ValidatedData<DisplayAddExamConfigurationPageResponseMessage>>
  {
    private readonly DisplayAddExamTypePageRequestMessageValidator _validator;
    private readonly IDisplayAddExamTypePageUseCase _useCase;


    public DisplayAddExamTypePageRequestHandler(DisplayAddExamTypePageRequestMessageValidator validator,
      IDisplayAddExamTypePageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddExamConfigurationPageResponseMessage>> Handle(DisplayAddExamConfigurationPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddExamConfigurationPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddExamConfigurationPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayAddExamTypePage(request);

      returnMessage = new ValidatedData<DisplayAddExamConfigurationPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}