using System.Collections.Generic;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public interface IGetFeatureTypeListUseCase
  {
    List<GetFeatureTypeListResponseMessage.FeatureType> GetFeatureTypeList(GetFeatureTypeListRequestMessage message);
  }
}