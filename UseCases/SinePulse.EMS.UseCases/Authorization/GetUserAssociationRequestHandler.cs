using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.UseCases.Authorization
{
  public class GetUserAssociationRequestHandler : IRequestHandler<GetUserAssociationRequestMessage,
    ValidatedData<GetUserAssociationResponseMessage>>
  {
    private readonly GetUserAssociationRequestMessageValidator _validator;
    private readonly IGetUserAssociationUseCase _useCase;

    public GetUserAssociationRequestHandler(GetUserAssociationRequestMessageValidator validator,
      IGetUserAssociationUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ValidatedData<GetUserAssociationResponseMessage>> Handle(GetUserAssociationRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<GetUserAssociationResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<GetUserAssociationResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var userAssociation = _useCase.GetUserAssociation(request);

      returnMessage = new ValidatedData<GetUserAssociationResponseMessage>(validationResult, userAssociation);
      return Task.FromResult(returnMessage);
    }
  }
}