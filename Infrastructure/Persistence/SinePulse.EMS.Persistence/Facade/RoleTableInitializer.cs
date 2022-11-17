using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Persistence.Facade
{
  public class RoleTableInitializer : IRoleTableInitializer
  {
    private readonly EmsDbContext _dbContext;

    public RoleTableInitializer(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void InitializeRoleTable(AuditFields auditFields)
    {
      if (IsAlreadyCreatedRoles())
      {
        return;
      }

      CreateFeaturesAndRoles();
      _dbContext.SaveChanges();
    }

    private bool IsAlreadyCreatedRoles()
    {
      return _dbContext.Set<Role>().Any();
    }

    private void CreateFeaturesAndRoles()
    {
      var superAdminRole = CreateRole(RoleNames.SuperAdmin, RoleTypeEnum.SuperAdmin);
      var instituteAdminRole = CreateRole(RoleNames.InstituteAdmin, RoleTypeEnum.InstituteAdmin);
      var branchAdminRole = CreateRole(RoleNames.BranchAdmin, RoleTypeEnum.BranchAdmin);
      var branchMediumAdminRole = CreateRole(RoleNames.BranchMediumAdmin, RoleTypeEnum.BranchMediumAdmin);
      var branchMediumUserRole = CreateRole(RoleNames.BranchMediumUser, RoleTypeEnum.BranchMediumUser);

      var generalSetupFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.GeneralSetup);
      CreateGeneralSetupFeature(generalSetupFeatureType, superAdminRole);

      var academicSetupFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.AcademicSetup);
      CreateAcademicSetupFeature(academicSetupFeatureType, superAdminRole);

      var employeeSetupFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.EmployeeSetup);
      CreateEmployeeSetupFeature(employeeSetupFeatureType, superAdminRole);

      var userManagementFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.UserManagement);
      CreateUserManagementFeature(userManagementFeatureType, new[] {superAdminRole});

      var instituteSuperAdminFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Institute);
      CreateInstituteSuperAdminFeature(instituteSuperAdminFeatureType, new[] { superAdminRole });

      var instituteFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Institute);
      CreateInstituteFeature(instituteFeatureType, new[] {superAdminRole, instituteAdminRole });

      var superAdminFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.EmployeeSuperAdmin);
      CreateSuperAdminEmployeeFeature(superAdminFeatureType, new[] { superAdminRole });

      var sessionFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Session);
      CreateSessionFeature(sessionFeatureType, new[] {superAdminRole, instituteAdminRole, branchAdminRole, branchMediumAdminRole, branchMediumUserRole });

      var branchFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Branch);
      CreateBranchFeature(branchFeatureType, new[] {superAdminRole, instituteAdminRole, branchAdminRole});

      var branchMediumFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.BranchMedium);
      CreateBranchMediumFeature(branchMediumFeatureType,
        new[] {superAdminRole, instituteAdminRole, branchAdminRole, branchMediumAdminRole, branchMediumUserRole });

      var sectionFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Section);
      CreateSectionFeature(sectionFeatureType,
        new[] {superAdminRole, instituteAdminRole, branchAdminRole, branchMediumAdminRole, branchMediumUserRole});

      var examinationFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Examination);
      CreateExaminationFeature(examinationFeatureType,
        new[] {superAdminRole, instituteAdminRole, branchAdminRole, branchMediumAdminRole, branchMediumUserRole});

      var employeeFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Employee);
      CreateEmployeeFeature(employeeFeatureType, new[] {superAdminRole, instituteAdminRole, branchAdminRole, branchMediumAdminRole, branchMediumUserRole });

      var studentFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Student);
      CreateStudentFeature(studentFeatureType,
        new[] {superAdminRole, instituteAdminRole, branchAdminRole, branchMediumAdminRole, branchMediumUserRole});

      var accountingFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.Accounting);
      CreateAccountingFeature(accountingFeatureType,
        new[] { superAdminRole, instituteAdminRole, branchAdminRole, branchMediumAdminRole, branchMediumUserRole });

      var financialStatementFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.FinancialStatements);
      CreateFinancialStatementsFeature(financialStatementFeatureType,
        new[] { superAdminRole, instituteAdminRole, branchAdminRole, branchMediumAdminRole, branchMediumUserRole });

      var bankInfoFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.BankInfo);
      CreateBankInfoFeature(bankInfoFeatureType,
        new[] { superAdminRole, instituteAdminRole, branchAdminRole, branchMediumAdminRole, branchMediumUserRole });

      var SuperAdminServiceFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.SuperAdminServiceType);
      CreateSuperAdminServiceFeature(SuperAdminServiceFeatureType, new[] { superAdminRole});

      var serviceFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.ServiceChargeType);
      CreateServiceChargeFeature(serviceFeatureType, new[] { superAdminRole, instituteAdminRole, branchAdminRole, branchMediumAdminRole });

      var SuperAdminPayrollFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.SuperAdminPayroll);
      CreateSuperAdminPayrollFeature(SuperAdminPayrollFeatureType, new[] { superAdminRole });

      var PayrollFeatureType = CreateFeatureType(FeatureType.FeatureTypeEnum.payroll);
      CreatePayrollFeature(PayrollFeatureType, 
        new[] { superAdminRole,  instituteAdminRole, branchAdminRole, branchMediumAdminRole });
    }

    private Role CreateRole(string roleName, RoleTypeEnum roleType)
    {
      var role = new Role
      {
        Name = roleName,
        NormalizedName = roleName,
        RoleType = roleType
      };
      var roleStore = new RoleStore<Role>(_dbContext);
      roleStore.CreateAsync(role).GetAwaiter().GetResult();
      return role;
    }

    private FeatureType CreateFeatureType(FeatureType.FeatureTypeEnum featureTypeEnum)
    {
      var featureType = new FeatureType
      {
        FeatureTypeName = Enum.GetName(typeof(FeatureType.FeatureTypeEnum), featureTypeEnum),
        FeatureTypeCode = featureTypeEnum.GetHashCode()
      };
      _dbContext.FeatureTypes.Add(featureType);
      return featureType;
    }

    private void CreateAcademicSetupFeature(FeatureType academicSetupFeatureType, Role role)
    {
      var values = Enum.GetValues(typeof(Feature.AcademicSetupEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = academicSetupFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.AcademicSetupEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        CreateRoleFeatures(feature, role);
      }
    }

    private void CreateGeneralSetupFeature(FeatureType generalSetupFeatureType, Role role)
    {
      var values = Enum.GetValues(typeof(Feature.GeneralSetupEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = generalSetupFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.GeneralSetupEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        CreateRoleFeatures(feature, role);
      }
    }

    private void CreateEmployeeSetupFeature(FeatureType employeeSetupFeatureType, Role role)
    {
      var values = Enum.GetValues(typeof(Feature.EmployeeSetupEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = employeeSetupFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.EmployeeSetupEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        CreateRoleFeatures(feature, role);
      }
    }

    private void CreateUserManagementFeature(FeatureType userManagementFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.UserManagementEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = userManagementFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.UserManagementEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }

    private void CreateSessionFeature(FeatureType sessionFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.SessionEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = sessionFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.SessionEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }
    private void CreateInstituteSuperAdminFeature(FeatureType instituteSuperAdminFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.InstituteSuperAdminEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = instituteSuperAdminFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.InstituteSuperAdminEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }
    private void CreateInstituteFeature(FeatureType instituteFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.InstituteEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = instituteFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.InstituteEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }
    private void CreateSuperAdminEmployeeFeature(FeatureType superAdminFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.EmployeeSuperAdminEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = superAdminFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.EmployeeSuperAdminEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }
    private void CreateBranchFeature(FeatureType branchFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.BranchEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = branchFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.BranchEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }

    private void CreateBranchMediumFeature(FeatureType branchMediumFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.BranchMediumEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = branchMediumFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.BranchMediumEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }

    private void CreateSectionFeature(FeatureType sectionFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.SectionEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = sectionFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.SectionEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }

    private void CreateExaminationFeature(FeatureType examinationFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.ExaminationEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = examinationFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.ExaminationEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }

    private void CreateEmployeeFeature(FeatureType employeeFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.EmployeeEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = employeeFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.EmployeeEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }

    private void CreateStudentFeature(FeatureType studentFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.StudentEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = studentFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.StudentEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }
    private void CreateAccountingFeature(FeatureType accountingFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.AccountingEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = accountingFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.AccountingEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }
    private void CreateFinancialStatementsFeature(FeatureType financialStatementFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.FinancialStatementEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = financialStatementFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.FinancialStatementEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }
    private void CreateBankInfoFeature(FeatureType bankInfoFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.BankInfoEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = bankInfoFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.BankInfoEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }

    private void CreateSuperAdminServiceFeature(FeatureType SuperAdminServiceFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.SuperAdminServiceChargeEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = SuperAdminServiceFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.SuperAdminServiceChargeEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }
    private void CreateServiceChargeFeature(FeatureType serviceFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.ServiceChargeEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = serviceFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.ServiceChargeEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }
    private void CreateSuperAdminPayrollFeature(FeatureType SuperAdminPayrollFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.SuperAdminPayrollEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = SuperAdminPayrollFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.SuperAdminPayrollEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }

    private void CreatePayrollFeature(FeatureType PayrollFeatureType, Role[] roles)
    {
      var values = Enum.GetValues(typeof(Feature.PayrollEnum));
      foreach (int value in values)
      {
        var feature = new Feature
        {
          FeatureType = PayrollFeatureType,
          FeatureName = Enum.GetName(typeof(Feature.PayrollEnum), value),
          FeatureCode = value
        };

        _dbContext.Features.Add(feature);
        foreach (var role in roles)
        {
          CreateRoleFeatures(feature, role);
        }
      }
    }
    private void CreateRoleFeatures(Feature feature, Role role)
    {
      var roleFeature = new RoleFeature {Feature = feature, Role = role};
      _dbContext.RoleFeatures.Add(roleFeature);
    }
  }
}