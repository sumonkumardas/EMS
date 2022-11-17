using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Features;

namespace SinePulse.EMS.Messages.FeatureMessages
{
  public class GetFeaturesAndAssignedUsersOfRoleResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public FeaturesAndUsers FeaturesAndUser { get; }

    public GetFeaturesAndAssignedUsersOfRoleResponseMessage(ValidationResult validationResult, FeaturesAndUsers featuresAndUser)
    {
      ValidationResult = validationResult;
      FeaturesAndUser = featuresAndUser;
    }
    
    public class FeaturesAndUsers
    {
      public List<Feature> Features { get; set; }
      public List<User> Users { get; set; }
    }

    public class Feature
    {
      public long FeatureId { get; set; }
      public string FeatureName { get; set; }
      public int FeatureCode { get; set; }
      public FeatureType FeatureType { get; set; }
    }

    public class User
    {
      public long EmployeeId { get; set; }
      public string EmployeeName { get; set; }
    }
  }
}