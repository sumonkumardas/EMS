using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public interface IAddFeatureInRoleUseCase
  {
    void AddFeatureInRole(AddFeatureInRoleRequestMessage message);
  }
}