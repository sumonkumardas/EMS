using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.FeatureMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Features
{
  public class GetFeatureTypeListUseCase : IGetFeatureTypeListUseCase
  {
    private readonly IRepository _repository;

    public GetFeatureTypeListUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<GetFeatureTypeListResponseMessage.FeatureType> GetFeatureTypeList(
      GetFeatureTypeListRequestMessage message)
    {
      var featureTypes = _repository.Table<FeatureType>().ToList();
      var requestedFeatureTypes = new List<GetFeatureTypeListResponseMessage.FeatureType>();
      foreach (var featureType in featureTypes)
      {
        requestedFeatureTypes.Add(new GetFeatureTypeListResponseMessage.FeatureType
        {
          FeatureTypeId = featureType.Id,
          FeatureTypeName = featureType.FeatureTypeName
        });
      }

      return requestedFeatureTypes;
    }
  }
}