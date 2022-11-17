using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class AddSubjectRequestHandler : IRequestHandler<AddSubjectRequestMessage, AddSubjectResponseMessage>
  {
    private readonly AddSubjectRequestMessageValidator _validator;
    private readonly IAddSubjectUseCase _useCase;

    public AddSubjectRequestHandler(AddSubjectRequestMessageValidator validator, IAddSubjectUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<AddSubjectResponseMessage> Handle(AddSubjectRequestMessage request, CancellationToken cancellationToken)
    {
      AddSubjectResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddSubjectResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var subject = _useCase.AddSubject(request);

      returnMessage = new AddSubjectResponseMessage(validationResult, subject.Id);
      return Task.FromResult(returnMessage);
    }
  }
}