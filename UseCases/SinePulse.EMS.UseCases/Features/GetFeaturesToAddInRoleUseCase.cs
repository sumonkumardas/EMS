using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.FeatureMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Features
{
  public class GetFeaturesToAddInRoleUseCase : IGetFeaturesToAddInRoleUseCase
  {
    private readonly IRepository _repository;

    public GetFeaturesToAddInRoleUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<GetFeaturesToAddInRoleResponseMessage.Feature> GetFeaturesToAddInRole(
      GetFeaturesToAddInRoleRequestMessage message)
    {
      var features = _repository.Table<Feature>()
        .Where(f => f.FeatureType.Id == message.FeatureTypeId)
        .ToList();
      
      var addedFeatures = _repository.Table<RoleFeature>()
        .Include(nameof(Feature))
        .Where(r => r.Role.Id == message.RoleId)
        .Select(r => r.Feature)
        .ToList();
      var common = features.Intersect(addedFeatures).ToList();
      features.RemoveAll(x => common.Contains(x));
      var requestedFeatures = ToRequestedFeatures(features);
      return requestedFeatures;
    }

    private List<GetFeaturesToAddInRoleResponseMessage.Feature> ToRequestedFeatures(List<Feature> filteredFeatures)
    {
      var requestedFeatures = new List<GetFeaturesToAddInRoleResponseMessage.Feature>();
      if (filteredFeatures != null && filteredFeatures.Any())
      {
        foreach (var feature in filteredFeatures)
        {
          requestedFeatures.Add(new GetFeaturesToAddInRoleResponseMessage.Feature
          {
            FeatureId = feature.Id,
            FeatureName = feature.FeatureName
          });
        }
      }

      return requestedFeatures;
    }
  }
}