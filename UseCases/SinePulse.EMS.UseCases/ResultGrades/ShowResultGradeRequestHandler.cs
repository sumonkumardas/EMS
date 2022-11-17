using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class ShowResultGradeRequestHandler
    : IRequestHandler<ShowResultGradeRequestMessage, ValidatedData<ShowResultGradeResponseMessage>>
  {
    private readonly ShowResultGradeRequestMessageValidator _validator;
    private readonly IShowResultGradeUseCase _useCase;

    public ShowResultGradeRequestHandler(ShowResultGradeRequestMessageValidator validator,
      IShowResultGradeUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowResultGradeResponseMessage>> Handle(ShowResultGradeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowResultGradeResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowResultGradeResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowResultGrade(request);

      returnMessage = new ValidatedData<ShowResultGradeResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}