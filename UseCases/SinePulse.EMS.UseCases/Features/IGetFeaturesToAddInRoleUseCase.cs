using System.Collections.Generic;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public interface IGetFeaturesToAddInRoleUseCase
  {
    List<GetFeaturesToAddInRoleResponseMessage.Feature> GetFeaturesToAddInRole(
      GetFeaturesToAddInRoleRequestMessage message);
  }
}