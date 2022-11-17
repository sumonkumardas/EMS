using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public class PromoteStudentOptionRequestHandler
    : IRequestHandler<PromoteStudentOptionRequestMessage, ValidatedData<PromoteStudentOptionResponseMessage>>
  {
    private readonly PromoteStudentOptionRequestMessageValidator _validator;
    private readonly IPromoteStudentOptionUseCase _useCase;


    public PromoteStudentOptionRequestHandler(PromoteStudentOptionRequestMessageValidator validator,
      IPromoteStudentOptionUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<PromoteStudentOptionResponseMessage>> Handle(PromoteStudentOptionRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<PromoteStudentOptionResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<PromoteStudentOptionResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.PromoteStudentOption(request);

      returnMessage = new ValidatedData<PromoteStudentOptionResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}