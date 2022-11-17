using System.Collections.Generic;
using FluentValidation.Results;

namespace SinePulse.EMS.Messages.FeatureMessages
{
  public class GetFeaturesInRoleResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<Feature> Features { get; }

    public GetFeaturesInRoleResponseMessage(ValidationResult validationResult, List<Feature> features)
    {
      ValidationResult = validationResult;
      Features = features;
    }

    public class Feature
    {
      public string FeatureName { get; set; }
      public long FeatureId { get; set; }
    }
  }
}