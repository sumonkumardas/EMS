using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ContactInfoMessages;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public class AddContactInfoRequestHandler : IRequestHandler<AddContactInfoRequestMessage, AddContactInfoResponseMessage>
  {
    private readonly AddContactInfoRequestMessageValidator _validator;
    private readonly IAddContactInfoUseCase _addContactInfoUseCase;

    public AddContactInfoRequestHandler(AddContactInfoRequestMessageValidator validator,
      IAddContactInfoUseCase addContactInfoUseCase)
    {
      _validator = validator;
      _addContactInfoUseCase = addContactInfoUseCase;
    }

    public Task<AddContactInfoResponseMessage> Handle(AddContactInfoRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddContactInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddContactInfoResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var contactInfo = _addContactInfoUseCase.AddContactInfo(request);

      returnMessage = new AddContactInfoResponseMessage(validationResult, contactInfo.Id);
      return Task.FromResult(returnMessage);
    }
  }
}