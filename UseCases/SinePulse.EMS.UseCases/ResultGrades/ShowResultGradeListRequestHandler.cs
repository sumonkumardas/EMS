using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class ShowResultGradeListRequestHandler
    : IRequestHandler<ShowResultGradeListRequestMessage, ValidatedData<ShowResultGradeListResponseMessage>>
  {
    private readonly ShowResultGradeListRequestMessageValidator _validator;
    private readonly IShowResultGradeListUseCase _useCase;

    public ShowResultGradeListRequestHandler(ShowResultGradeListRequestMessageValidator validator,
      IShowResultGradeListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<ShowResultGradeListResponseMessage>> Handle(ShowResultGradeListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<ShowResultGradeListResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<ShowResultGradeListResponseMessage>(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var responseMessage = _useCase.ShowResultGradeList(request);

      returnMessage = new ValidatedData<ShowResultGradeListResponseMessage>(validationResult, responseMessage);
      return Task.FromResult(returnMessage);
    }
  }
}