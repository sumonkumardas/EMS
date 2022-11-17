using SinePulse.EMS.Messages.AuthorizationMessages;

namespace SinePulse.EMS.UseCases.Authorization
{
  public interface IGetUserAssociationUseCase
  {
    GetUserAssociationResponseMessage GetUserAssociation(GetUserAssociationRequestMessage request);
  }
}