using System.Collections.Generic;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public interface IGetFeaturesInRoleUseCase
  {
    List<GetFeaturesInRoleResponseMessage.Feature> GetFeaturesInRole(GetFeaturesInRoleRequestMessage message);
  }
}