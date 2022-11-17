using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Students
{
  public class DisplayAddStudentPageRequestHandler
    : IRequestHandler<DisplayAddStudentPageRequestMessage, ValidatedData<DisplayAddStudentPageResponseMessage>>
  {
    private readonly DisplayAddStudentPageRequestMessageValidator _validator;
    private readonly IDisplayAddStudentPageUseCase _useCase;


    public DisplayAddStudentPageRequestHandler(DisplayAddStudentPageRequestMessageValidator validator,
      IDisplayAddStudentPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayAddStudentPageResponseMessage>> Handle(DisplayAddStudentPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayAddStudentPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayAddStudentPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayAddStudentPage(request);

      returnMessage = new ValidatedData<DisplayAddStudentPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}