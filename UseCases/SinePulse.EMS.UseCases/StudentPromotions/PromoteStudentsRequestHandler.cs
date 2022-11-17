using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public class PromoteStudentsRequestHandler
    : IRequestHandler<PromoteStudentsRequestMessage, ValidatedData<PromoteStudentsResponseMessage>>
  {
    private readonly PromoteStudentsRequestMessageValidator _validator;
    private readonly IPromoteStudentsUseCase _useCase;


    public PromoteStudentsRequestHandler(PromoteStudentsRequestMessageValidator validator,
      IPromoteStudentsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<PromoteStudentsResponseMessage>> Handle(PromoteStudentsRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<PromoteStudentsResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<PromoteStudentsResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.PromoteStudents(request);

      returnMessage = new ValidatedData<PromoteStudentsResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}