using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class ShowEducationalQualificationRequestHandler : IRequestHandler<ShowEducationalQualificationRequestMessage,
    ShowEducationalQualificationResponseMessage>
  {
    private readonly ShowEducationalQualificationRequestMessageValidator _validator;
    private readonly IShowEducationalQualificationUseCase _useCase;

    public ShowEducationalQualificationRequestHandler(ShowEducationalQualificationRequestMessageValidator validator,
      IShowEducationalQualificationUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowEducationalQualificationResponseMessage> Handle(ShowEducationalQualificationRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowEducationalQualificationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowEducationalQualificationResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var educationalQualification = _useCase.ShowEducationalQualification(request);

      returnMessage = new ShowEducationalQualificationResponseMessage(validationResult, educationalQualification);
      return Task.FromResult(returnMessage);
    }
  }
}