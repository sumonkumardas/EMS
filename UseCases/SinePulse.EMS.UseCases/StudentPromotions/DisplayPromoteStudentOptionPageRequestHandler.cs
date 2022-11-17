using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public class DisplayPromoteStudentOptionPageRequestHandler
    : IRequestHandler<DisplayPromoteStudentOptionPageRequestMessage, 
      ValidatedData<DisplayPromoteStudentOptionPageResponseMessage>>
  {
    private readonly DisplayPromoteStudentOptionPageRequestMessageValidator _validator;
    private readonly IDisplayPromoteStudentOptionPageUseCase _useCase;


    public DisplayPromoteStudentOptionPageRequestHandler(DisplayPromoteStudentOptionPageRequestMessageValidator validator,
      IDisplayPromoteStudentOptionPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayPromoteStudentOptionPageResponseMessage>> Handle(
      DisplayPromoteStudentOptionPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayPromoteStudentOptionPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayPromoteStudentOptionPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayPromoteStudentOptionPage(request);

      returnMessage = new ValidatedData<DisplayPromoteStudentOptionPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}