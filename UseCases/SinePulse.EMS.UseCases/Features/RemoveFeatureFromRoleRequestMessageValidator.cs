using FluentValidation;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.UseCases.Features
{
  public class RemoveFeatureFromRoleRequestMessageValidator : AbstractValidator<RemoveFeatureFromRoleRequestMessage>
  {
    public RemoveFeatureFromRoleRequestMessageValidator()
    {
      RuleFor(x => x.FeatureIds).Must((m, x) => IsValidFeatureIds(m.FeatureIds)).WithMessage("Select Feature.");
    }

    private bool IsValidFeatureIds(long[] featureIds)
    {
      if (featureIds != null && featureIds.Length > 0)
      {
        foreach (var featureId in featureIds)
        {
          if (featureId <= 0)
            return false;
        }
      }
      else
      {
        return false;
      }

      return true;
    }
  }
}