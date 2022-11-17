using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassRoutineMessages;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class GetAddClassRoutineViewModelDataRequestHandler : IRequestHandler<GetAddClassRoutineViewModelDataRequestMessage, GetAddClassRoutineViewModelDataResponseMessage>
  {
    private readonly GetAddClassRoutineViewModelDataRequestMessageValidator _validator;
    private readonly IGetAddClassRoutineViewModelDataUseCase _useCase;

    public GetAddClassRoutineViewModelDataRequestHandler(GetAddClassRoutineViewModelDataRequestMessageValidator validator,
      IGetAddClassRoutineViewModelDataUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetAddClassRoutineViewModelDataResponseMessage> Handle(GetAddClassRoutineViewModelDataRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetAddClassRoutineViewModelDataResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetAddClassRoutineViewModelDataResponseMessage(validationResult, null, null, null);
        return Task.FromResult(returnMessage);
      }

      _useCase.GetAddClassRoutineViewModelData(request);

      returnMessage = new GetAddClassRoutineViewModelDataResponseMessage(validationResult, request.Rooms, 
        request.Teachers, request.Subjects);
      return Task.FromResult(returnMessage);
    }
  }
}