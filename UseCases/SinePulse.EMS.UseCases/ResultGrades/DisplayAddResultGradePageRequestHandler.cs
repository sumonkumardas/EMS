using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class DisplayAddResultGradePageRequestHandler
    : IRequestHandler<DisplayAddResultGradePageRequestMessage, ValidatedData<DisplayAddResultGradePageResponseMessage>>
  {
    private readonly DisplayAddResultGradePageRequestMessageValidator _validator;
    private readonly IDisplayAddResultGradePageUseCase _useCase;


    public DisplayAddResultGradePageRequestHandler(DisplayAddResultGradePageRequestMessageValidator validator,
      IDisplayAddResultGradePageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddResultGradePageResponseMessage>> Handle(DisplayAddResultGradePageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddResultGradePageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddResultGradePageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayAddResultGradePage(request);

      returnMessage = new ValidatedData<DisplayAddResultGradePageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}