using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace SinePulse.EMS.Site.Models
{
  public class BaseViewModel
  {
    [IgnoreMap]
    public string LoginName { get; set; }
    [IgnoreMap]
    public virtual string LoginImageUrl { get; set; }
    [IgnoreMap]
    public AssociationType AssociatedWith { get; set; }
    [IgnoreMap]
    public long? LoginUsersInstituteId { get; set; }
    [IgnoreMap]
    public long? LoginUsersBranchId { get; set; }
    [IgnoreMap]
    public long? LoginUsersBranchMediumId { get; set; }
    [IgnoreMap]
    public IList<RoleFeature> RoleFeatures { get; set; } = new List<RoleFeature>();
    [IgnoreMap]
    public byte[] InstituteBanner { get; set; }
    [IgnoreMap]
    public decimal PendingAmount { get; set; }
    [IgnoreMap]
    public bool IsBillDueDateOver { get; set; }

    public bool IsBillPending()
    {
      if (RoleFeatures.Any())
      {
        if(RoleFeatures[0].Role.RoleType == ProjectPrimitives.RoleTypeEnum.BranchMediumAdmin ||
          RoleFeatures[0].Role.RoleType == ProjectPrimitives.RoleTypeEnum.BranchMediumUser)
        {
          if (PendingAmount > 0) return true;
        }
      }
      return false;
    }
    public bool HasPermission(string featureName)
    {
      return RoleFeatures.Any(a => a.Feature.FeatureName == featureName);
    }
    public bool HasMediumSetupPermission()
    {
      List<string> mediumSetupFeaturesName = new List<string>(new string[] 
      {
        Feature.AcademicSetupEnum.NewMedium.ToString(),
        Feature.AcademicSetupEnum.ViewMedium.ToString(),
        Feature.AcademicSetupEnum.EditMedium.ToString(),
        Feature.AcademicSetupEnum.FindMedium.ToString()
      });
      return RoleFeatures.Any(a => mediumSetupFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasSubjectSetupPermission()
    {
      List<string> mediumSetupFeaturesName = new List<string>(new string[]
      {
        Feature.AcademicSetupEnum.NewSubject.ToString(),
        Feature.AcademicSetupEnum.ViewSubject.ToString(),
        Feature.AcademicSetupEnum.EditSubject.ToString(),
        Feature.AcademicSetupEnum.FindSubject.ToString()
      });
      return RoleFeatures.Any(a => mediumSetupFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasClassSetupPermission()
    {
      List<string> mediumSetupFeaturesName = new List<string>(new string[]
      {
        Feature.AcademicSetupEnum.NewClass.ToString(),
        Feature.AcademicSetupEnum.ViewClass.ToString(),
        Feature.AcademicSetupEnum.EditClass.ToString(),
        Feature.AcademicSetupEnum.FindClass.ToString()
      });
      return RoleFeatures.Any(a => mediumSetupFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasAcademicPermission()
    {
      return HasMediumSetupPermission() & HasSubjectSetupPermission() & HasClassSetupPermission();
    }
    public bool HasPublicHolidaySetupPermission()
    {
      List<string> publicHolidaySetupFeaturesName = new List<string>(new string[]
      {
        Feature.GeneralSetupEnum.NewPublicHoliday.ToString(),
        Feature.GeneralSetupEnum.EditPublicHoliday.ToString(),
        Feature.GeneralSetupEnum.ViewPublicHoliday.ToString(),
        Feature.GeneralSetupEnum.FindPublicHoliday.ToString()
      });
      return RoleFeatures.Any(a => publicHolidaySetupFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasDesignationSetupPermission()
    {
      List<string> designationSetupFeaturesName = new List<string>(new string[]
      {
        Feature.EmployeeSetupEnum.NewDesignation.ToString(),
        Feature.EmployeeSetupEnum.EditDesignation.ToString(),
        Feature.EmployeeSetupEnum.ViewDesignation.ToString(),
        Feature.EmployeeSetupEnum.FindDesignation.ToString()
      });
      return RoleFeatures.Any(a => designationSetupFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasJobTypeSetupPermission()
    {
      List<string> jobTypeSetupFeaturesName = new List<string>(new string[]
      {
        Feature.EmployeeSetupEnum.NewJobType.ToString(),
        Feature.EmployeeSetupEnum.EditJobType.ToString(),
        Feature.EmployeeSetupEnum.ViewJobType.ToString(),
        Feature.EmployeeSetupEnum.FindJobType.ToString()
      });
      return RoleFeatures.Any(a => jobTypeSetupFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasGradeSetupPermission()
    {
      List<string> gradeSetupFeaturesName = new List<string>(new string[]
      {
        Feature.EmployeeSetupEnum.NewGrade.ToString(),
        Feature.EmployeeSetupEnum.EditGrade.ToString(),
        Feature.EmployeeSetupEnum.ViewGrade.ToString(),
        Feature.EmployeeSetupEnum.FindGrade.ToString()
      });
      return RoleFeatures.Any(a => gradeSetupFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasLeaveTypeSetupPermission()
    {
      List<string> leaveTypeSetupFeaturesName = new List<string>(new string[]
      {
        Feature.EmployeeSetupEnum.NewLeaveType.ToString(),
        Feature.EmployeeSetupEnum.EditLeaveType.ToString(),
        Feature.EmployeeSetupEnum.ViewLeaveType.ToString(),
        Feature.EmployeeSetupEnum.FindLeaveType.ToString()
      });
      return RoleFeatures.Any(a => leaveTypeSetupFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasEmployeeSetupPermission()
    {
      return HasDesignationSetupPermission() & HasJobTypeSetupPermission() & HasGradeSetupPermission() & HasLeaveTypeSetupPermission();
    }
    public bool HasAnySetupPermission()
    {
      return HasAcademicPermission() & HasEmployeeSetupPermission() & HasPublicHolidaySetupPermission();
    }
    public bool HasInstituteCreationPermission()
    {
      List<string> instituteCreationFeaturesName = new List<string>(new string[]
      {
        Feature.InstituteSuperAdminEnum.NewInstitute.ToString(),
        Feature.InstituteSuperAdminEnum.FindInstitute.ToString()
      });
      return RoleFeatures.Any(a => instituteCreationFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasProfileManagementPermission()
    {
      List<string> profileManagementFeaturesName = new List<string>(new string[]
      {
        Feature.UserManagementEnum.AddRole.ToString(),
        Feature.UserManagementEnum.ShowAllRoles.ToString(),
        Feature.UserManagementEnum.AssignFeatureToRole.ToString(),
        Feature.UserManagementEnum.ShowFeatures.ToString()
      });
      return RoleFeatures.Any(a => profileManagementFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasSuperAdminAccess()
    {
      List<string> superAdminFeaturesName = new List<string>(new string[]
      {
        Feature.EmployeeSuperAdminEnum.AddSuperAdmin.ToString(),
        Feature.EmployeeSuperAdminEnum.FindSuperAdmin.ToString()
      });
      return RoleFeatures.Any(a => superAdminFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasBillingSetupPermission()
    {
      List<string> BillingSetupFeaturesName = new List<string>(new string[]
      {
        Feature.SuperAdminServiceChargeEnum.AddServiceCharge.ToString(),
        Feature.SuperAdminServiceChargeEnum.EditServiceCharge.ToString(),
        Feature.SuperAdminServiceChargeEnum.ViewServiceCharge.ToString(),
        Feature.SuperAdminServiceChargeEnum.FindServiceCharge.ToString(),
        Feature.SuperAdminServiceChargeEnum.ShowPaymentHistory.ToString(),
        Feature.SuperAdminServiceChargeEnum.ShowPendingBills.ToString()
      });
      return RoleFeatures.Any(a => BillingSetupFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasSalaryComponentTypeSetupPermission()
    {
      List<string> salaryComponentTypeSetupFeaturesName = new List<string>(new string[]
      {
        Feature.SuperAdminPayrollEnum.AddSalaryComponentType.ToString(),
        Feature.SuperAdminPayrollEnum.EditSalaryComponentType.ToString(),
        Feature.SuperAdminPayrollEnum.FindSalaryComponentType.ToString(),
        Feature.SuperAdminPayrollEnum.DeleteSalaryComponentType.ToString()
      });
      return RoleFeatures.Any(a => salaryComponentTypeSetupFeaturesName.Contains(a.Feature.FeatureName));
    }
    public bool HasSalaryComponentSetupPermission()
    {
      List<string> salaryComponentSetupFeaturesName = new List<string>(new string[]
      {
        Feature.SuperAdminPayrollEnum.AddSalaryComponent.ToString(),
        Feature.SuperAdminPayrollEnum.EditSalaryComponent.ToString(),
        Feature.SuperAdminPayrollEnum.FindSalaryComponent.ToString(),
        Feature.SuperAdminPayrollEnum.DeleteSalaryComponent.ToString()
      });
      return RoleFeatures.Any(a => salaryComponentSetupFeaturesName.Contains(a.Feature.FeatureName));
    }
  }
}
