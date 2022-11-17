using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public class DisplayPromoteStudentsPageRequestHandler
    : IRequestHandler<DisplayPromoteStudentsPageRequestMessage, 
      ValidatedData<DisplayPromoteStudentsPageResponseMessage>>
  {
    private readonly DisplayPromoteStudentsPageRequestMessageValidator _validator;
    private readonly IDisplayPromoteStudentsPageUseCase _useCase;


    public DisplayPromoteStudentsPageRequestHandler(DisplayPromoteStudentsPageRequestMessageValidator validator,
      IDisplayPromoteStudentsPageUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<DisplayPromoteStudentsPageResponseMessage>> Handle(
      DisplayPromoteStudentsPageRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<DisplayPromoteStudentsPageResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<DisplayPromoteStudentsPageResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.DisplayPromoteStudentsPage(request);

      returnMessage = new ValidatedData<DisplayPromoteStudentsPageResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}