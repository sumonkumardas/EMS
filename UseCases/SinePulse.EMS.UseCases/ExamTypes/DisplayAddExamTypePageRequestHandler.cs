using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class DisplayAddExamTypePageRequestHandler
    : IRequestHandler<DisplayAddExamTypePageRequestMessage, ValidatedData<DisplayAddExamTypePageResponseMessage>>
  {
    private readonly DisplayAddExamTypePageRequestMessageValidator _validator;
    private readonly IDisplayAddExamTypePageUseCase _useCase;


    public DisplayAddExamTypePageRequestHandler(DisplayAddExamTypePageRequestMessageValidator validator,
      IDisplayAddExamTypePageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddExamTypePageResponseMessage>> Handle(DisplayAddExamTypePageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddExamTypePageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddExamTypePageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayAddExamTypePage(request);

      returnMessage = new ValidatedData<DisplayAddExamTypePageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}