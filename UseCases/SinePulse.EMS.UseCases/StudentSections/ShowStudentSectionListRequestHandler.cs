using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentSectionMessages;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public class ShowStudentSectionListRequestHandler : IRequestHandler<ShowStudentSectionListRequestMessage, ShowStudentSectionListResponseMessage>
  {
    private readonly ShowStudentSectionListRequestMessageValidator _validator;
    private readonly IShowStudentSectionListUseCase _useCase;

    public ShowStudentSectionListRequestHandler(ShowStudentSectionListRequestMessageValidator validator, IShowStudentSectionListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowStudentSectionListResponseMessage> Handle(ShowStudentSectionListRequestMessage request, CancellationToken cancellationToken)
    {
      ShowStudentSectionListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowStudentSectionListResponseMessage(validationResult,new List<ShowStudentSectionListResponseMessage.Student>());
        return Task.FromResult(returnMessage);
      }

      var list = _useCase.ShowStudentSectionList(request);

      returnMessage = new ShowStudentSectionListResponseMessage(validationResult, list);
      return Task.FromResult(returnMessage);
    }
  }
}