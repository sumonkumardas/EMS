using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.FeatureMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Features
{
  public class GetFeaturesInRoleUseCase : IGetFeaturesInRoleUseCase
  {
    private readonly IRepository _repository;

    public GetFeaturesInRoleUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<GetFeaturesInRoleResponseMessage.Feature> GetFeaturesInRole(GetFeaturesInRoleRequestMessage message)
    {
      var addedFeatures = _repository.Table<RoleFeature>()
        .Include(nameof(Feature))
        .Where(r => r.Role.Id == message.RoleId
                    && r.Feature.FeatureType.Id == message.FeatureTypeId)
        .Select(r => r.Feature)
        .ToList();

      var requestedFeatures = new List<GetFeaturesInRoleResponseMessage.Feature>();
      foreach (var feature in addedFeatures)
      {
        requestedFeatures.Add(new GetFeaturesInRoleResponseMessage.Feature
        {
          FeatureName = feature.FeatureName,
          FeatureId = feature.Id
        });
      }

      return requestedFeatures;
    }
  }
}