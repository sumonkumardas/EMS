using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentSectionMessages;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public class ShowStudentSectionRollRequestHandler : IRequestHandler<ShowStudentSectionRollRequestMessage, ShowStudentSectionRollResponseMessage>
  {
    private readonly ShowStudentSectionRollRequestMessageValidator _validator;
    private readonly IShowStudentSectionRollUseCase _useCase;

    public ShowStudentSectionRollRequestHandler(ShowStudentSectionRollRequestMessageValidator validator, IShowStudentSectionRollUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }
     
    public Task<ShowStudentSectionRollResponseMessage> Handle(ShowStudentSectionRollRequestMessage request, CancellationToken cancellationToken)
    {
      ShowStudentSectionRollResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowStudentSectionRollResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var roll = _useCase.ShowStudentSectionRoll(request);

      returnMessage = new ShowStudentSectionRollResponseMessage(validationResult, roll);
      return Task.FromResult(returnMessage);
    }
  }
}