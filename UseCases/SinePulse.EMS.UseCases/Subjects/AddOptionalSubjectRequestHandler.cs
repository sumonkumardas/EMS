using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class AddOptionalSubjectRequestHandler :IRequestHandler<AddOptionalSubjectRequestMessage, AddOptionalSubjectResponseMessage>
  {
    private readonly AddOptionalSubjectRequestMessageValidator _validator;
    private readonly IAddOptionalSubjectUseCase _useCase;

    public AddOptionalSubjectRequestHandler(AddOptionalSubjectRequestMessageValidator validator, IAddOptionalSubjectUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddOptionalSubjectResponseMessage> Handle(AddOptionalSubjectRequestMessage request, CancellationToken cancellationToken)
    {
      AddOptionalSubjectResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddOptionalSubjectResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddOptionalSubject(request);

      returnMessage = new AddOptionalSubjectResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}