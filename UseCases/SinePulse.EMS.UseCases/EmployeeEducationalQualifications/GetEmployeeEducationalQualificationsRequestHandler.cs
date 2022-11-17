using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class GetEmployeeEducationalQualificationsRequestHandler : IRequestHandler<GetEmployeeEducationalQualificationsRequestMessage, GetEmployeeEducationalQualificationsResponseMessage>
  {
    private readonly GetEmployeeEducationalQualificationsRequestMessageValidator _validator;
    private readonly IGetEmployeeEducationalQualificationsUseCase _useCase;

    public GetEmployeeEducationalQualificationsRequestHandler(GetEmployeeEducationalQualificationsRequestMessageValidator validator,
      IGetEmployeeEducationalQualificationsUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<GetEmployeeEducationalQualificationsResponseMessage> Handle(GetEmployeeEducationalQualificationsRequestMessage request,
      CancellationToken cancellationToken)
    {
      GetEmployeeEducationalQualificationsResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new GetEmployeeEducationalQualificationsResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var educationalQualifications = _useCase.GetEducationalQualifications(request);

      returnMessage = new GetEmployeeEducationalQualificationsResponseMessage(validationResult, educationalQualifications);
      return Task.FromResult(returnMessage);
    }
  }
}