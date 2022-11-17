using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Features
{
  public class FeatureType : BaseEntity
  {
    #region Primitive Properties

    public virtual string FeatureTypeName { get; set; }

    public virtual int FeatureTypeCode { get; set; }

    #endregion

    #region Feature Type Enums

    public enum FeatureTypeEnum
    {
      AcademicSetup = 1,
      EmployeeSetup = 2,
      GeneralSetup = 3,
      Session = 4,
      Institute = 5,
      Branch = 6,
      BranchMedium = 7,
      Section = 8,
      Examination = 9,
      Student = 10,
      Employee = 11,
      UserManagement = 12,
      Accounting = 13,
      FinancialStatements = 14,
      BankInfo = 15,
      EmployeeSuperAdmin = 16,
      SuperAdminServiceType = 17,
      SuperAdminPayroll = 18,
      payroll = 19,
      ServiceChargeType = 20
    }

    #endregion
  }
}