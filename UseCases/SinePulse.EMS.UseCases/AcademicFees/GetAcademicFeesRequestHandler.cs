using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AcademicFeeMessages;

namespace SinePulse.EMS.UseCases.AcademicFees
{
  public class GetAcademicFeesRequestHandler : IRequestHandler<GetAcademicFeesRequestMessage, GetAcademicFeesResponseMessage>
  {
    private readonly GetAcademicFeesRequestMessageValidator _validator;
    private readonly IGetAcademicFeesUseCase _useCase;

    public GetAcademicFeesRequestHandler(GetAcademicFeesRequestMessageValidator validator,
      IGetAcademicFeesUseCase addAcademicFeeUseCase)
    {
      _validator = validator;
      _useCase = addAcademicFeeUseCase;
    }

    public Task<GetAcademicFeesResponseMessage> Handle(GetAcademicFeesRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetAcademicFeesResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetAcademicFeesResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var academicFees = _useCase.GetAcademicFees(request);

      returnMessage = new GetAcademicFeesResponseMessage(validationResult, academicFees);
      return Task.FromResult(returnMessage);
    }
  }
}