using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.WaiverMessages;

namespace SinePulse.EMS.UseCases.Waivers
{
  public class AddWaiverRequestMessageValidator : AbstractValidator<AddWaiverRequestMessage>
  {
    public AddWaiverRequestMessageValidator()
    {
      RuleFor(x => x).Must(x => x.SectionId > 0).WithMessage("Student does not belong to any Section.");
      RuleFor(x => x).Must((m, x) => IsWaiverAmountsValid(m.AcademicFees, m.Waivers))
        .WithMessage("Enter Valid Waiver Amounts/Percentages.");
      RuleFor(x => x).Must((m, x) => IsWaiverLessThanFees(m.AcademicFees, m.Waivers, m.InPercentagesBooleanArray))
        .WithMessage("Waiver Amount must be less than or equal to Fees.");
      RuleFor(x => x).Must((m, x) => IsWaiverPositive(m.Waivers))
        .WithMessage("Waiver Cannot be negative.");
      RuleFor(x => x).Must((m, x) => IsWaiverPercentagesValid(m.Waivers, m.InPercentagesBooleanArray))
        .WithMessage("Waiver Percentages cannot be more than 100.");
    }

    private bool IsWaiverPercentagesValid(decimal[] waivers, bool[] inPercentagesBooleanArray)
    {
      if (waivers == null  || waivers.Length != inPercentagesBooleanArray.Length)
        return true;
      var index = -1;
      foreach (var waiver in waivers)
      {
        if (inPercentagesBooleanArray[++index] && waiver > 100)
        {
          return false;
        }
      }

      return true;
    }

    private bool IsWaiverPositive(decimal[] waivers)
    {
      foreach (var waiver in waivers)
      {
        if (waiver < 0)
          return false;
      }

      return true;
    }

    private bool IsWaiverLessThanFees(IEnumerable<AcademicFeeMessageModel> academicFees, decimal[] waivers,
      bool[] inPercentagesBooleanArray)
    {
      if (waivers == null || academicFees == null || waivers.Length != academicFees.Count())
        return true;
      var index = -1;
      foreach (var academicFee in academicFees)
      {
        ++index;
        if (academicFee.Fees < waivers[index] && inPercentagesBooleanArray[index] == false)
          return false;
      }

      return true;
    }

    private bool IsWaiverAmountsValid(IEnumerable<AcademicFeeMessageModel> academicFees, decimal[] waivers)
    {
      return academicFees != null &&
             waivers != null &&
             waivers.Length == academicFees.Count() &&
             academicFees.Count() == waivers.Length;
    }
  }
}