using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.FeatureMessages
{
  public class GetFeatureTypeListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<FeatureType> FeatureTypes { get; }

    public GetFeatureTypeListResponseMessage(ValidationResult validationResult, List<FeatureType> featureTypes)
    {
      ValidationResult = validationResult;
      FeatureTypes = featureTypes;
    }

    public class FeatureType
    {
      public string FeatureTypeName { get; set; }
      public long FeatureTypeId { get; set; }
    }
  }
}