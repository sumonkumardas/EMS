using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ContactInfoMessages;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public class AddPresentAddressInContactInfoRequestHandler : IRequestHandler<AddPresentAddressInContactInfoRequestMessage, AddPresentAddressInContactInfoResponseMessage>
  {
    private readonly AddPresentAddressInContactInfoRequestMessageValidator _validator;
    private readonly IAddPresentAddressInContactInfoUseCase _useCase;

    public AddPresentAddressInContactInfoRequestHandler(AddPresentAddressInContactInfoRequestMessageValidator validator,
      IAddPresentAddressInContactInfoUseCase addPresentAddressInContactInfoUseCase)
    {
      _validator = validator;
      _useCase = addPresentAddressInContactInfoUseCase;
    }

    public Task<AddPresentAddressInContactInfoResponseMessage> Handle(AddPresentAddressInContactInfoRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddPresentAddressInContactInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddPresentAddressInContactInfoResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var contactInfo = _useCase.AddPresentAddressInContactInfo(request);

      returnMessage = new AddPresentAddressInContactInfoResponseMessage(validationResult, contactInfo.Id);
      return Task.FromResult(returnMessage);
    }
  }
}