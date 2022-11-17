using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ContactInfoMessages;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public class AddPermanentAddressInContactInfoRequestHandler : IRequestHandler<AddPermanentAddressInContactInfoRequestMessage, AddPermanentAddressInContactInfoResponseMessage>
  {
    private readonly AddPermanentAddressInContactInfoRequestMessageValidator _validator;
    private readonly IAddPermanentAddressInContactInfoUseCase _useCase;

    public AddPermanentAddressInContactInfoRequestHandler(AddPermanentAddressInContactInfoRequestMessageValidator validator,
      IAddPermanentAddressInContactInfoUseCase addPermanentAddressInContactInfoUseCase)
    {
      _validator = validator;
      _useCase = addPermanentAddressInContactInfoUseCase;
    }

    public Task<AddPermanentAddressInContactInfoResponseMessage> Handle(AddPermanentAddressInContactInfoRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddPermanentAddressInContactInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddPermanentAddressInContactInfoResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var contactInfo = _useCase.AddPermanentAddressInContactInfo(request);

      returnMessage = new AddPermanentAddressInContactInfoResponseMessage(validationResult, contactInfo.Id);
      return Task.FromResult(returnMessage);
    }
  }
}