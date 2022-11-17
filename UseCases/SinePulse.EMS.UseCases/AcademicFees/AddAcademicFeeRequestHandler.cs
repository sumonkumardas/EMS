using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AcademicFeeMessages;

namespace SinePulse.EMS.UseCases.AcademicFees
{
  public class AddAcademicFeeRequestHandler : IRequestHandler<AddAcademicFeeRequestMessage, AddAcademicFeeResponseMessage>
  {
    private readonly AddAcademicFeeRequestMessageValidator _validator;
    private readonly IAddAcademicFeeUseCase _addAcademicFeeUseCase;

    public AddAcademicFeeRequestHandler(AddAcademicFeeRequestMessageValidator validator,
      IAddAcademicFeeUseCase addAcademicFeeUseCase)
    {
      _validator = validator;
      _addAcademicFeeUseCase = addAcademicFeeUseCase;
    }

    public Task<AddAcademicFeeResponseMessage> Handle(AddAcademicFeeRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddAcademicFeeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddAcademicFeeResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _addAcademicFeeUseCase.AddAcademicFee(request);

      returnMessage = new AddAcademicFeeResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}