using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ContactInfoMessages;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public class ShowContactInfoRequestHandler : IRequestHandler<ShowContactInfoRequestMessage, ShowContactInfoResponseMessage>
  {
    private readonly ShowContactInfoRequestMessageValidator _validator;
    private readonly IShowContactInfoUseCase _addContactInfoUseCase;

    public ShowContactInfoRequestHandler(ShowContactInfoRequestMessageValidator validator,
      IShowContactInfoUseCase addContactInfoUseCase)
    {
      _validator = validator;
      _addContactInfoUseCase = addContactInfoUseCase;
    }

    public Task<ShowContactInfoResponseMessage> Handle(ShowContactInfoRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowContactInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowContactInfoResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var contactInfo = _addContactInfoUseCase.ShowContactInfo(request);

      returnMessage = new ShowContactInfoResponseMessage(validationResult, contactInfo);
      return Task.FromResult(returnMessage);
    }
  }
}