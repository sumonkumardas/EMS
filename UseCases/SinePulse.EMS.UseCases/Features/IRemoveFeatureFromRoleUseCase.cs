using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public interface IRemoveFeatureFromRoleUseCase
  {
    void RemoveFeatureFromRole(RemoveFeatureFromRoleRequestMessage message);
  }
}