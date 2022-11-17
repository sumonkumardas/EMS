using SinePulse.EMS.Domain.Shared;

namespace SinePulse.EMS.Domain.Features
{
  public class Feature : BaseEntity
  {
    #region Primitive Properties
    
    public virtual string FeatureName { get; set; }

    public virtual int FeatureCode { get; set; }
    
    #endregion

    #region Navigation Properties

    public virtual FeatureType FeatureType { get; set; }

    #endregion

    #region Feature Enums
    public enum GeneralSetupEnum
    {
      NewPublicHoliday = 1,
      ViewPublicHoliday = 2,
      EditPublicHoliday = 3,
      FindPublicHoliday = 4
    }
    public enum AcademicSetupEnum
    {
      NewMedium = 1,
      ViewMedium = 2,
      EditMedium = 3,
      FindMedium = 4,
      NewSubject = 5,
      ViewSubject = 6,
      EditSubject = 7,
      FindSubject = 8,
      NewClass = 9,
      ViewClass = 10,
      EditClass = 11,
      FindClass = 12,
      AddSubject = 13
    }
    public enum EmployeeSetupEnum
    {
      NewDesignation = 1,
      ViewDesignation = 2,
      EditDesignation = 3,
      FindDesignation = 4,
      NewJobType = 5,
      ViewJobType = 6,
      EditJobType = 7,
      FindJobType = 8,
      NewGrade = 9,
      ViewGrade = 10,
      EditGrade = 11,
      FindGrade = 12,
      NewLeaveType = 13,
      ViewLeaveType = 14,
      EditLeaveType = 15,
      FindLeaveType = 16
    }
    public enum EmployeeEnum
    {
      NewEmployee = 1,
      ViewEmployee = 2,
      EditEmployee = 3,
      FindEmployee = 4,
      AddPersonalDetail = 5,
      ViewPersonalDetail = 6,
      EditPersonalDetail = 7,
      AddEducationalInfo = 8,
      ViewEducationalInfo = 9,
      EditEducationalInfo = 10,
      AddAddress = 11,
      ViewAddress = 12,
      EditAddress = 13,
      AddOrChangePhoto = 14,
      ApplyLeave = 15,
      ApproveLeave = 16,
      AddLogin = 17,
      ChangePassword = 18,
      AddAttendance = 19,
      EditAttendace = 20,
      ViewAttendance = 21,
      FindAttendance = 22,
      ViewLeave = 23,
      EditLeave = 24,
      FindLeave = 25,
    }
    public enum EmployeeSuperAdminEnum
    {
      AddSuperAdmin = 1,
      ViewSuperAdmin = 2,
      EditSuperAdmin = 3,
      FindSuperAdmin = 4
    }
    public enum StudentEnum
    {
      NewAdmission = 1,
      ViewStudent = 2,
      UpdateStudent = 3,
      FindStudent = 4,
      AddOrChangePhoto = 5,
      AddSection = 6,
      AddContactInfo = 7,
      EditContactInfo = 8,
      ViewContactInfo = 9,
      FindContactInfo = 10,
      AddContactPerson = 11,
      EditContactPersion = 12,
      ViewContactPerson = 13,
      FindContactPerson = 14,
      AddAddress = 15,
      ViewAddress = 16,
      EditAddress = 17,
      AddAttendance = 18,
      EditAttendace = 13,
      ViewAttendance = 14,
      FindAttendance = 15,
      WaiveAcademicFees = 16,
      AddOptionalSubject = 17
    }
    public enum SessionEnum
    {
      NewSession = 1,
      ViewSession = 2,
      EditSession = 3,
      FindSession = 4,
      SetASCurrentSession = 5
    }
    public enum InstituteEnum
    {
      ViewInstitute = 1,
      NewCommitteeMember = 2,
      ViewCommitteeMember = 3,
      EditCommitteeMember = 4,
      FindCommitteeMember = 5,
      AddCommitteeMemberPersonalInfo = 6,
      ViewCommitteeMemberPersonalInfo = 7,
      AddCommitteeMemberAddress = 8,
      ViewCommitteeMemberAddress = 9
    }
    public enum InstituteSuperAdminEnum
    {
      NewInstitute = 1,
      EditInstitute = 2,
      FindInstitute = 3,
    }
    public enum BranchEnum
    {
      AddBranch = 1,
      ViewBranch = 2,
      EditBranch = 3,
      FindBranch = 4,
      AddShift = 5,
      ViewShift = 6,
      EditShift = 7,
      FindShift = 8,
      AddRoom = 9,
      EditRoom = 10,
      ViewRoom = 11
    }
    public enum BranchMediumEnum
    {
      AddBranchMedium = 1,
      ViewBranchMedium = 2,
      EditBranchMedium = 3,
      FindBranchMedium = 4,
      AddClassBreakDown = 5,
      ViewClassBreakDown = 6,
      EditClassBreakDown = 7,
      FindClassBreakDown = 8,
      SetAcademicFees = 13,
      ImportData = 14,
      ChangeSessionBufferPeriod = 15,
      ClearData = 16,
      AcademicCalendar = 17,
      FindAcademicFees = 18,
      ViewAcademicFees = 19,
      CollectAcademicFees = 20,
      AddNotificationConfiguration = 21,
      EditNotificationConfiguration = 22,
      ViewNotificationConfiguration = 23
    }
    public enum BankInfoEnum
    {
      AddBankInfo = 1,
      EditBankInfo = 2,
      ViewBankInfo = 3,
      FindBankInfo = 4,
      AddBankBranch = 5,
      EditBankBranch = 6,
      ViewBankBranch = 7,
      FindBankBranch = 8,
      AddAccountNumber = 9,
      EditAccountNumber = 10,
      ViewAccountNumber = 11,
      FindAccountNumber = 12,
      CreateCOA = 13,
      ViewBankBranchAddress = 14
    }
    public enum SectionEnum
    {
      NewSection = 1,
      ViewSection = 2,
      EditSection = 3,
      FindSection = 4,
      AssignOrChangeRoom = 5,
      AddClassRoutine = 6,
      EditClassRoutine = 7,
      ViewClassRouting = 8,
      FindClassRoutine = 9
    }
    public enum ExaminationEnum
    {
      NewExamTerm = 1,
      ViewExamTerm = 2,
      EditExamTerm = 3,
      FindExamTerm = 4,
      AddExamConfiguration = 5,
      ViewExamConfiguration = 6,
      EditExamConfiguration = 7,
      FindExamConfiguration = 8,
      AddExamRoutine = 9,
      EditExamRoutine = 10,
      FindExamRoutine = 11,
      AddClassTest = 12,
      ViewClassTest = 13,
      EditClassTest = 14,
      FindClassTest = 15,
      PrepareResult = 16,
      AddSeatPlan = 17,
      EditSeatPlan = 18,
      ViewSeatPlan = 19,
      FindSeatPlan = 20,
      AddResultGrade = 21,
      EditResultGrade = 22,
      ViewResultGrade = 23,
      FindResultGrade = 24,
      AddMark = 25,
      EditMark = 26,
      FindMark = 27,
      ViewMark = 28
    }
    public enum UserManagementEnum
    {
      AddRole = 1,
      EditRole = 2,
      AssignRoleToUser = 3,
      AssignFeatureToRole = 4,
      RemoveFeatureFromRole = 5,
      ShowAllRoles = 6,
      ShowFeatures = 7
    }
    public enum AccountingEnum
    {
      GeneralJournal = 1,
      CashVoucher = 2,
      BankVoucher = 3,
      ContraJournal = 4,
      VoucherEdit = 5,
      VoucherView = 6,
      COAEntry = 7,
      COAEdit = 8,
      COAView = 9,
      YearClosing = 10,
      OpeningBalance = 11,
      ImportCOA = 12,
      AccountView = 13,
      ShowTransaction = 14
    }
    public enum FinancialStatementEnum
    {
      TrialBalance = 1,
      IncomeStatement = 2,
      BalanceSheet = 3
    }
    public enum SuperAdminServiceChargeEnum
    {
      AddServiceCharge = 1,
      ViewServiceCharge = 2,
      EditServiceCharge = 3,
      FindServiceCharge = 4,
      ShowPaymentHistory = 5,
      ShowPendingBills = 6
    }

    public enum ServiceChargeEnum
    {
      PayOnline = 1,
      BillingView = 2
    }

    public enum SuperAdminPayrollEnum
    {
      AddSalaryComponentType = 1,
      EditSalaryComponentType = 2,
      DeleteSalaryComponentType = 3,
      FindSalaryComponentType = 4,

      AddSalaryComponent = 5,
      EditSalaryComponent = 6,
      DeleteSalaryComponent = 7,
      FindSalaryComponent = 8,
    }

    public enum PayrollEnum
    {
      DefineSalary = 1,
      EditSalaryComponent = 2,
      GenerateSalarySheet = 3
    }
    #endregion
  }
}