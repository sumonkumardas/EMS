using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Accounts;
using SinePulse.EMS.NO.Domain.Designations;
using SinePulse.EMS.NO.Domain.Employees;
using SinePulse.EMS.NO.Domain.Features;
using SinePulse.EMS.NO.Domain.Holidays;
using SinePulse.EMS.NO.Domain.Leaves;
using SinePulse.EMS.NO.Domain.UserManagement;
using SinePulse.EMS.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Facade
{
  public class SampleDbInitializer : DropCreateDatabaseIfModelChanges<EducationManagementDbContext>
  {
    protected override void Seed(EducationManagementDbContext context)
    {
      CreateFeaturesAndRoles(context);

      CreateAssetTypeAccounts(context);
      CreateLiabilitiesTypeAccounts(context);
      CreateEquityTypeAccounts(context);
      CreateRevenueTypeAccounts(context);
      CreateExpenseTypeAccounts(context);

      NOMedium banglaMedium = CreateMedium("Bangla", "01", context);
      NOMedium englishMedium = CreateMedium("English Version", "02", context);

      NOSubject bangla = CreateSubject("Bangla", "01", context);
      NOSubject english = CreateSubject("English", "02", context);
      NOSubject math = CreateSubject("Math", "03", context);
      NOSubject science = CreateSubject("Science", "04", context);

      NOSubject bangla1stPaper = CreateSubject("Bangla 1st Paper", "101", context);
      NOSubject bangla2ndPaper = CreateSubject("Bangla 2nd Paper", "102", context);
      NOSubject english1stPaper = CreateSubject("English 1st Paper", "103", context);
      NOSubject English2ndPaper = CreateSubject("English 2nd Paper", "104", context);
      NOSubject GeneralMath = CreateSubject("General Math", "105", context);
      NOSubject physics = CreateSubject("Physic", "106", context);
      NOSubject chemestry = CreateSubject("Chemestry", "107", context);
      NOSubject biology = CreateSubject("Biology", "108", context);
      NOSubject geography = CreateSubject("Geography", "109", context);
      NOSubject higherMath = CreateSubject("Higher Math", "110", context);

      NOClass classOne = CreateClass("One", "01", context);
      AddSubjectToClass(classOne, GroupEnum.AllGroup, bangla, 100, 40, context);
      AddSubjectToClass(classOne, GroupEnum.AllGroup, english, 100, 40, context);
      AddSubjectToClass(classOne, GroupEnum.AllGroup, math, 100, 40, context);
      AddSubjectToClass(classOne, GroupEnum.AllGroup, science, 100, 40, context);

      NOClass classFive = CreateClass("Five", "05", context);
      AddSubjectToClass(classFive, GroupEnum.AllGroup, bangla, 100, 40, context);
      AddSubjectToClass(classFive, GroupEnum.AllGroup, english, 100, 40, context);
      AddSubjectToClass(classFive, GroupEnum.AllGroup, math, 100, 40, context);
      AddSubjectToClass(classFive, GroupEnum.AllGroup, science, 100, 40, context);

      NOClass classNine = CreateClass("Nine", "09", context);
      AddSubjectToClass(classNine, GroupEnum.AllGroup, bangla1stPaper, 100, 40, context);
      AddSubjectToClass(classNine, GroupEnum.AllGroup, bangla2ndPaper, 100, 40, context);
      AddSubjectToClass(classNine, GroupEnum.AllGroup, english1stPaper, 100, 40, context);
      AddSubjectToClass(classNine, GroupEnum.AllGroup, English2ndPaper, 100, 40, context);
      AddSubjectToClass(classNine, GroupEnum.AllGroup, GeneralMath, 100, 40, context);
      AddSubjectToClass(classNine, GroupEnum.Science, physics, 100, 40, context);
      AddSubjectToClass(classNine, GroupEnum.Science, chemestry, 100, 40, context);
      AddSubjectToClass(classNine, GroupEnum.Science, biology, 100, 40, context);
      AddSubjectToClass(classNine, GroupEnum.Science, geography, 100, 40, context);

      CreateDesignation("HeadMaster", EmployeeTypeEnum.Teacher, context);
      CreateDesignation("Asst. Teacher", EmployeeTypeEnum.Teacher, context);
      CreateDesignation("Senior Teacher", EmployeeTypeEnum.Teacher, context);
      CreateDesignation("Officer", EmployeeTypeEnum.Staff, context);
      CreateDesignation("Accountant", EmployeeTypeEnum.Staff, context);

      CreateJobType("Permanemt", false, context);
      CreateJobType("Part Time", true, context);

      CreateEmployeeGrade("Grade 1", 15000, 20000, context);
      CreateEmployeeGrade("Grade 2", 10000, 14999, context);
      CreateEmployeeGrade("Grade 3", 5000, 9999, context);

      CreateLeaveType("Annual Leave", 15, false, true, 100, context);
      CreateLeaveType("Casual Leave", 12, false, false, 0, context);
      CreateLeaveType("Medical Leave", 15, true, false, 0, context);

      CreatePublicHoliday("International Language Day", new DateTime(2019, 02, 21), new DateTime(2019, 02, 21), context);
      CreatePublicHoliday("Independance Day", new DateTime(2019, 03, 26), new DateTime(2019, 03, 26), context);
      CreatePublicHoliday("Victory Day", new DateTime(2019, 12, 16), new DateTime(2019, 12, 16), context);
    }

    private void CreateAssetTypeAccounts(EducationManagementDbContext context)
    {
      NOAccountType assetAccountType = CreateAccountType(ChartOfAccountTypeEnum.Assets, FinancialStatementsEnum.BalanceSheet, 1000, 1999, context);

      NOAccountHead assetAccountHead = CreateAccountHead("1000", "Assets", assetAccountType, null, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);

      NOAccountHead currentAssetAccountHead = CreateAccountHead("1100", "Current Assets", assetAccountType, assetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead bankAccountHead = CreateAccountHead("1110", "Bank", assetAccountType, currentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      ConfigureAutoPosting(VoucherTypeEnum.BankVoucher, bankAccountHead, context);
      NOAccountHead cashOnHandAccountHead = CreateAccountHead("1120", "Cash on Hand", assetAccountType, currentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true, context);
      ConfigureAutoPosting(VoucherTypeEnum.CashVoucher, cashOnHandAccountHead, context);
      NOAccountHead pettyCashAccountHead = CreateAccountHead("1130", "Petty Cash", assetAccountType, currentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true, context);
      NOAccountHead accountsReceivableAccountHead = CreateAccountHead("1140", "Debtors/Accounts Receivable", assetAccountType, currentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead stockOnHandAccountHead = CreateAccountHead("1150", "Stock on Hand", assetAccountType, currentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);

      NOAccountHead noncurrentAssetAccountHead = CreateAccountHead("1200", "Non-Current Assets", assetAccountType, assetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead furnitureAssetAccountHead = CreateAccountHead("1210", "Furniture and Fittings", assetAccountType, noncurrentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead officeEquipmentAssetAccountHead = CreateAccountHead("1220", "Office Equipments", assetAccountType, noncurrentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead companyCarAssetAccountHead = CreateAccountHead("1230", "Company Car", assetAccountType, noncurrentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);

      CreateAccountTransactionType(assetAccountType, AccountTransactionTypeEnum.Debit, IncreaseDecreaseEnum.Increase, context);
      CreateAccountTransactionType(assetAccountType, AccountTransactionTypeEnum.Credit, IncreaseDecreaseEnum.Decrease, context);

      CreateAccountTypeVoucherRelationship(VoucherTypeEnum.CashVoucher, assetAccountType, context);
      CreateAccountTypeVoucherRelationship(VoucherTypeEnum.BankVoucher, assetAccountType, context);
    }
    private void CreateLiabilitiesTypeAccounts(EducationManagementDbContext context)
    {
      NOAccountType liabilityAccountType = CreateAccountType(ChartOfAccountTypeEnum.Liabilities, FinancialStatementsEnum.BalanceSheet, 2000, 2999, context);

      NOAccountHead liabilityAccountHead = CreateAccountHead("2000", "Liabilities", liabilityAccountType, null, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);

      NOAccountHead currentLiabilityAccountHead = CreateAccountHead("2100", "Current Liabilities", liabilityAccountType, liabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead bankOverdraftAccountHead = CreateAccountHead("2110", "Bank Overdraft", liabilityAccountType, currentLiabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead accountsPayableAccountHead = CreateAccountHead("2120", "Creditors/Accounts Payable", liabilityAccountType, currentLiabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);

      NOAccountHead nonCurrentLiabilityAccountHead = CreateAccountHead("2200", "Non-Current Liabilities", liabilityAccountType, liabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead companyCarLoanAccountHead = CreateAccountHead("2210", "Company Car Loan", liabilityAccountType, nonCurrentLiabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead equipmentLoanAccountHead = CreateAccountHead("2220", "Equipment Loan", liabilityAccountType, nonCurrentLiabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead longTermLoanLoanAccountHead = CreateAccountHead("2230", "Long Term Loan", liabilityAccountType, nonCurrentLiabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);

      CreateAccountTransactionType(liabilityAccountType, AccountTransactionTypeEnum.Debit, IncreaseDecreaseEnum.Decrease, context);
      CreateAccountTransactionType(liabilityAccountType, AccountTransactionTypeEnum.Credit, IncreaseDecreaseEnum.Increase, context);
    }
    private void CreateEquityTypeAccounts(EducationManagementDbContext context)
    {
      NOAccountType equityAccountType = CreateAccountType(ChartOfAccountTypeEnum.Equity, FinancialStatementsEnum.BalanceSheet, 3000, 3999, context);

      NOAccountHead equityAccountHead =  CreateAccountHead("3000", "Equity", equityAccountType, null, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead OwnerCapitalAccountHead = CreateAccountHead("3110", "Owner's Capital", equityAccountType, equityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead retainedEarningAccountHead = CreateAccountHead("3210", "Retained Earnings", equityAccountType, equityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead currentProfitAccountHead = CreateAccountHead("3310", "Current Profit", equityAccountType, equityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);

      CreateAccountTransactionType(equityAccountType, AccountTransactionTypeEnum.Debit, IncreaseDecreaseEnum.Decrease, context);
      CreateAccountTransactionType(equityAccountType, AccountTransactionTypeEnum.Credit, IncreaseDecreaseEnum.Increase, context);
    }
    private void CreateRevenueTypeAccounts(EducationManagementDbContext context)
    {
      NOAccountType incomeAccountType = CreateAccountType(ChartOfAccountTypeEnum.Income, FinancialStatementsEnum.IncomeStatement, 4000, 4999, context);

      NOAccountHead revenueAccountHead = CreateAccountHead("4000", "Revenue", incomeAccountType, null, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead feesAccountHead = CreateAccountHead("4100", "Fees", incomeAccountType, revenueAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.None, false, context);

      NOAccountHead monthlyFeesAccountHead = CreateAccountHead("4110", "Monthly Fees", incomeAccountType, feesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Monthly, false, context);
      NOAccountHead tuitionFeesAccountHead = CreateAccountHead("4111", "Tuition Fees", incomeAccountType, monthlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Monthly, true, context);
      NOAccountHead absentFeesAccountHead = CreateAccountHead("4112", "Absent Fees", incomeAccountType, monthlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Monthly, true, context);
      NOAccountHead ictFeesAccountHead = CreateAccountHead("4113", "ICT Fees", incomeAccountType, monthlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Monthly, true, context);

      NOAccountHead yearlyFeesAccountHead = CreateAccountHead("4120", "Yearly Fees", incomeAccountType, feesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Yearly, false, context);
      NOAccountHead admissionFeesAccountHead = CreateAccountHead("4121", "Admission Fees", incomeAccountType, yearlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Yearly, true, context);
      NOAccountHead developmentFeesAccountHead = CreateAccountHead("4122", "Development Fees", incomeAccountType, yearlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Yearly, true, context);
      NOAccountHead idCardFeesAccountHead = CreateAccountHead("4123", "ID Card Fees", incomeAccountType, yearlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Yearly, true, context);
      NOAccountHead examFeesAccountHead = CreateAccountHead("4124", "Exam Fees", incomeAccountType, yearlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Yearly, true, context);

      CreateAccountTransactionType(incomeAccountType, AccountTransactionTypeEnum.Debit, IncreaseDecreaseEnum.Decrease, context);
      CreateAccountTransactionType(incomeAccountType, AccountTransactionTypeEnum.Credit, IncreaseDecreaseEnum.Increase, context);

      CreateAccountTypeVoucherRelationship(VoucherTypeEnum.CashVoucher, incomeAccountType, context);
      CreateAccountTypeVoucherRelationship(VoucherTypeEnum.BankVoucher, incomeAccountType, context);
    }
    private void CreateExpenseTypeAccounts(EducationManagementDbContext context)
    {
      NOAccountType expenseAccountType = CreateAccountType(ChartOfAccountTypeEnum.Expense, FinancialStatementsEnum.IncomeStatement, 5000, 5999, context);

      NOAccountHead expenseAccountHead = CreateAccountHead("5000", "Expense", expenseAccountType, null, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);

      NOAccountHead fixedExpenseAccountHead = CreateAccountHead("5100", "Fixed", expenseAccountType, expenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead rentExpenseAccountHead = CreateAccountHead("5110", "Rent", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead houseRentExpenseAccountHead = CreateAccountHead("5111", "House Rent", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true, context);

      NOAccountHead salaryExpenseAccountHead = CreateAccountHead("5120", "Wages/Salaries", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead carExpenseAccountHead = CreateAccountHead("5130", "Company Car Expenses", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead hostingExpenseAccountHead = CreateAccountHead("5140", "Website Hosting", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);

      NOAccountHead utilityExpenseAccountHead = CreateAccountHead("5150", "Utilities", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead telephoneExpenseAccountHead = CreateAccountHead("5151", "Telephone Bills", expenseAccountType, utilityExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true, context);
      NOAccountHead mobileExpenseAccountHead = CreateAccountHead("5152", "Mobile Bills", expenseAccountType, utilityExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true, context);
      NOAccountHead electricityExpenseAccountHead = CreateAccountHead("5153", "Electricity Bills", expenseAccountType, utilityExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true, context);
      NOAccountHead internetExpenseAccountHead = CreateAccountHead("5154", "Internet Bills", expenseAccountType, utilityExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true, context);

      NOAccountHead variableExpenseAccountHead = CreateAccountHead("5200", "Variable", expenseAccountType, expenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead advertisingExpenseAccountHead = CreateAccountHead("5210", "Advertising", expenseAccountType, variableExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);
      NOAccountHead freightExpenseAccountHead = CreateAccountHead("5220", "Freight", expenseAccountType, variableExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false, context);

      CreateAccountTransactionType(expenseAccountType, AccountTransactionTypeEnum.Debit, IncreaseDecreaseEnum.Increase, context);
      CreateAccountTransactionType(expenseAccountType, AccountTransactionTypeEnum.Credit, IncreaseDecreaseEnum.Decrease, context);

      CreateAccountTypeVoucherRelationship(VoucherTypeEnum.CashVoucher, expenseAccountType, context);
      CreateAccountTypeVoucherRelationship(VoucherTypeEnum.BankVoucher, expenseAccountType, context);
    }
    private NOAccountHead CreateAccountHead(string accountCode, string headName, NOAccountType accountType, NOAccountHead parentHead, AccountHeadTypeEnum headType, AccountPeriodEnum period, bool isLedger, EducationManagementDbContext context)
    {
      NOAccountHead accountHead = new NOAccountHead();
      accountHead.AccountCode = accountCode;
      accountHead.AccountHeadName = headName;
      accountHead.AccountType = accountType;
      accountHead.ParentHead = parentHead;
      accountHead.AccountHeadType = headType;
      accountHead.AccountPeriod = period;
      accountHead.IsLedger = isLedger;
      accountHead.Status = StatusEnum.Active;

      accountHead.AuditFields.InsertedBy = "Automated";
      accountHead.AuditFields.InsertedDateTime = DateTime.Now;
      accountHead.AuditFields.LastUpdatedBy = "Automated";
      accountHead.AuditFields.LastUpdatedDateTime = DateTime.Now;

      context.AccountHeads.Add(accountHead);

      return accountHead;
    }
    private void CreateAccountTransactionType(NOAccountType accountType, AccountTransactionTypeEnum debit, IncreaseDecreaseEnum increaseDecreaseType, EducationManagementDbContext context)
    {
      NOAccountTransactionType accountTransactionType = new NOAccountTransactionType();
      accountTransactionType.AccountType = accountType;
      accountTransactionType.AccountTransactionType = debit;
      accountTransactionType.IncreaseDecreaseType = increaseDecreaseType;

      accountTransactionType.AuditFields.InsertedBy = "Automated";
      accountTransactionType.AuditFields.InsertedDateTime = DateTime.Now;
      accountTransactionType.AuditFields.LastUpdatedBy = "Automated";
      accountTransactionType.AuditFields.LastUpdatedDateTime = DateTime.Now;

      context.AccountTransactionTypes.Add(accountTransactionType);
    }
    private NOAccountType CreateAccountType(ChartOfAccountTypeEnum accountTypeEnum, FinancialStatementsEnum financialStatementType, int codingStartValue, int codingEndValue, EducationManagementDbContext context)
    {
      NOAccountType accountType = new NOAccountType();
      accountType.AccountTypeName = accountTypeEnum;
      accountType.FinancialStatement = financialStatementType;
      accountType.CodingStartValue = codingStartValue;
      accountType.CodingEndValue = codingEndValue;

      accountType.AuditFields.InsertedBy = "Automated";
      accountType.AuditFields.InsertedDateTime = DateTime.Now;
      accountType.AuditFields.LastUpdatedBy = "Automated";
      accountType.AuditFields.LastUpdatedDateTime = DateTime.Now;

      context.AccountTypes.Add(accountType);

      return accountType;
    }
    private void CreateAccountTypeVoucherRelationship(VoucherTypeEnum voucherType, NOAccountType accountType, EducationManagementDbContext context)
    {
      NOVoucherAccountTypeRelationship relationship = new NOVoucherAccountTypeRelationship();
      relationship.VoucherType = voucherType;
      relationship.AccountType = accountType;

      relationship.AuditFields.InsertedBy = "Automated";
      relationship.AuditFields.InsertedDateTime = DateTime.Now;
      relationship.AuditFields.LastUpdatedBy = "Automated";
      relationship.AuditFields.LastUpdatedDateTime = DateTime.Now;

      context.VoucherAccountTypeRelationships.Add(relationship);
    }
    private void ConfigureAutoPosting(VoucherTypeEnum voucherType, NOAccountHead accountHead, EducationManagementDbContext context)
    {
      NOAutoPostingConfig relationship = new NOAutoPostingConfig();
      relationship.VoucherType = voucherType;
      relationship.AccountHead = accountHead;

      relationship.AuditFields.InsertedBy = "Automated";
      relationship.AuditFields.InsertedDateTime = DateTime.Now;
      relationship.AuditFields.LastUpdatedBy = "Automated";
      relationship.AuditFields.LastUpdatedDateTime = DateTime.Now;

      context.AutoPostingConfigs.Add(relationship);
    }
    private void CreateFeaturesAndRoles(EducationManagementDbContext context)
    {
      NORole superAdminRole = CreateRole("SuperAdmin", context);
      NORole instituteAdminRole = CreateRole("InstituteAdmin", context);
      NORole branchAdminRole = CreateRole("BranchAdmin", context);
      NORole branchUserRole = CreateRole("BranchUser", context);

      NOLoginUser superAdmin = CreateSuperAdminUser(superAdminRole, context);
      AssignRoleToAdminUser(superAdmin, superAdminRole, context);

      NOFeatureType generalSetupFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.GeneralSetup, context);
      CreateGeneralSetupFeature(generalSetupFeatureType, superAdminRole, context);

      NOFeatureType academicSetupFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.AcademicSetup, context);
      CreateAcademicSetupFeature(academicSetupFeatureType, superAdminRole, context);

      NOFeatureType employeeSetupFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.EmployeeSetup, context);
      CreateEmployeeSetupFeature(academicSetupFeatureType, superAdminRole, context);

      NOFeatureType userManagementFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.UserManagement, context);
      CreateUserManagementFeature(userManagementFeatureType, new NORole[] { superAdminRole, instituteAdminRole, branchAdminRole }, context);

      NOFeatureType instituteFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.Institute, context);
      CreateInstituteFeature(instituteFeatureType, new NORole[] { superAdminRole, instituteAdminRole }, context);

      NOFeatureType sessionFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.Session, context);
      CreateSessionFeature(sessionFeatureType, new NORole[] { superAdminRole, instituteAdminRole, branchAdminRole }, context);

      NOFeatureType branchFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.Branch, context);
      CreateBranchFeature(branchFeatureType, new NORole[] { superAdminRole, instituteAdminRole, branchAdminRole }, context);

      NOFeatureType branchMediumFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.BranchMedium, context);
      CreateBranchMediumFeature(branchMediumFeatureType, new NORole[] { superAdminRole, instituteAdminRole, branchAdminRole }, context);

      NOFeatureType sectionFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.Section, context);
      CreateSectionFeature(sectionFeatureType, new NORole[] { superAdminRole, instituteAdminRole, branchAdminRole, branchUserRole }, context);

      NOFeatureType examinationFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.Examination, context);
      CreateExaminationFeature(examinationFeatureType, new NORole[] { superAdminRole, instituteAdminRole, branchAdminRole, branchUserRole }, context);

      NOFeatureType employeeFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.Employee, context);
      CreateEmployeeFeature(employeeFeatureType, new NORole[] { superAdminRole, instituteAdminRole, branchAdminRole }, context);

      NOFeatureType studentFeatureType = CreateFeatureType(NOFeatureType.FeatureTypeEnum.Student, context);
      CreateStudentFeature(studentFeatureType, new NORole[] { superAdminRole, instituteAdminRole, branchAdminRole, branchUserRole }, context);
    }
    private NORole CreateRole(string roleName, EducationManagementDbContext context)
    {
      NORole role = new NORole();
      role.Id = Guid.NewGuid().ToString();
      role.Name = roleName;

      context.Roles.Add(role);

      return role;
    }
    private NOLoginUser CreateSuperAdminUser(NORole role, EducationManagementDbContext context)
    {
      string userCode = "737466";

      NOEmployee employee = new NOEmployee();
      employee.UserCode = userCode;
      employee.FullName = "SinePulse";
      employee.EmailAddress = "smart.ems.admin@sinepulse.com";
      employee.EmployeeType = 0;
      employee.Status = StatusEnum.Active;
      employee.AuditFields.InsertedBy = "Automated";
      employee.AuditFields.InsertedDateTime = DateTime.Now;
      employee.AuditFields.LastUpdatedBy = "Automated";
      employee.AuditFields.LastUpdatedDateTime = DateTime.Now;
      context.Employees.Add(employee);

      NOLoginUser loginUser = CreateLoginUser(userCode, "123456", context);

      employee.LoginUser = loginUser;
      return loginUser;
    }
    private NOLoginUser CreateLoginUser(string loginId, string password, EducationManagementDbContext context)
    {
      NOLoginUser loginUser = new NOLoginUser();
      loginUser.Id = Guid.NewGuid().ToString();
      loginUser.UserName = loginId;
      loginUser.Email = loginId;
      loginUser.EmailConfirmed = false;
      loginUser.PasswordHash = PasswordHash.HashPassword(password);
      loginUser.SecurityStamp = Guid.NewGuid().ToString();
      loginUser.PhoneNumberConfirmed = false;
      loginUser.TwoFactorEnabled = false;
      loginUser.LockoutEnabled = false;
      loginUser.AccessFailedCount = 0;

      context.LoginUsers.Add(loginUser);

      return loginUser;
    }
    private void AssignRoleToAdminUser(NOLoginUser adminUser, NORole role, EducationManagementDbContext context)
    {
      NOUserRoles userRole = new NOUserRoles();
      userRole.LoginUser = adminUser;
      userRole.Role = role;

      context.UserRoles.Add(userRole);
    }
    private NOFeatureType CreateFeatureType(NOFeatureType.FeatureTypeEnum featureTypeEnum, EducationManagementDbContext context)
    {
      NOFeatureType featureType = new NOFeatureType();
      featureType.FeatureTypeName = Enum.GetName(typeof(NOFeatureType.FeatureTypeEnum), featureTypeEnum);
      featureType.FeatureTypeCode = featureTypeEnum.GetHashCode();

      context.FeatureTypes.Add(featureType);
      return featureType;
    }
    private void CreateAcademicSetupFeature(NOFeatureType academicSetupFeatureType, NORole role, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.AcademicSetupEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = academicSetupFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.AcademicSetupEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        CreateRoleFeatures(feature, role, context);
      }
    }
    private void CreateGeneralSetupFeature(NOFeatureType generalSetupFeatureType, NORole role, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.GeneralSetupEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = generalSetupFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.GeneralSetupEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        CreateRoleFeatures(feature, role, context);
      }
    }
    private void CreateEmployeeSetupFeature(NOFeatureType employeeSetupFeatureType, NORole role, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.EmployeeSetupEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = employeeSetupFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.EmployeeSetupEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        CreateRoleFeatures(feature, role, context);
      }
    }
    private void CreateUserManagementFeature(NOFeatureType userManagementFeatureType, NORole[] roles, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.UserManagementEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = userManagementFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.UserManagementEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        foreach (NORole role in roles)
        {
          CreateRoleFeatures(feature, role, context);
        }
      }
    }
    private void CreateSessionFeature(NOFeatureType sessionFeatureType, NORole[] roles, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.SessionEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = sessionFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.SessionEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        foreach (NORole role in roles)
        {
          CreateRoleFeatures(feature, role, context);
        }
      }
    }
    private void CreateInstituteFeature(NOFeatureType instituteFeatureType, NORole[] roles, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.InstituteEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = instituteFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.InstituteEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        foreach (NORole role in roles)
        {
          CreateRoleFeatures(feature, role, context);
        }
      }
    }
    private void CreateBranchFeature(NOFeatureType branchFeatureType, NORole[] roles, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.BranchEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = branchFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.BranchEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        foreach (NORole role in roles)
        {
          CreateRoleFeatures(feature, role, context);
        }
      }
    }
    private void CreateBranchMediumFeature(NOFeatureType branchMediumFeatureType, NORole[] roles, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.BranchMediumEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = branchMediumFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.BranchMediumEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        foreach (NORole role in roles)
        {
          CreateRoleFeatures(feature, role, context);
        }
      }
    }
    private void CreateSectionFeature(NOFeatureType sectionFeatureType, NORole[] roles, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.SectionEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = sectionFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.SectionEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        foreach (NORole role in roles)
        {
          CreateRoleFeatures(feature, role, context);
        }
      }
    }
    private void CreateExaminationFeature(NOFeatureType examinationFeatureType, NORole[] roles, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.ExaminationEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = examinationFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.ExaminationEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        foreach (NORole role in roles)
        {
          CreateRoleFeatures(feature, role, context);
        }
      }
    }
    private void CreateEmployeeFeature(NOFeatureType employeeFeatureType, NORole[] roles, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.EmployeeEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = employeeFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.EmployeeEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        foreach (NORole role in roles)
        {
          CreateRoleFeatures(feature, role, context);
        }
      }
    }
    private void CreateStudentFeature(NOFeatureType studentFeatureType, NORole[] roles, EducationManagementDbContext context)
    {
      var values = Enum.GetValues(typeof(NOFeature.StudentEnum));
      foreach (int value in values)
      {
        NOFeature feature = new NOFeature();
        feature.FeatureType = studentFeatureType;
        feature.FeatureName = Enum.GetName(typeof(NOFeature.StudentEnum), value);
        feature.FeatureCode = value;

        context.Features.Add(feature);
        foreach (NORole role in roles)
        {
          CreateRoleFeatures(feature, role, context);
        }
      }
    }

    private void CreateRoleFeatures(NOFeature feature, NORole role, EducationManagementDbContext context)
    {
      NORoleFeatures roleFeature = new NORoleFeatures();
      roleFeature.Feature = feature;
      roleFeature.Role = role;

      context.RoleFeatures.Add(roleFeature);
    }

    private NOMedium CreateMedium(string mediumName, string mediumCode, EducationManagementDbContext context)
    {
      NOMedium medium = new NOMedium();
      medium.MediumName = mediumName;
      medium.MediumCode = mediumCode;
      medium.Status = StatusEnum.Active;
      medium.AuditFields.InsertedBy = "Automated";
      medium.AuditFields.InsertedDateTime = DateTime.Now;
      medium.AuditFields.LastUpdatedBy = "Automated";
      medium.AuditFields.LastUpdatedDateTime = DateTime.Now;
      context.Mediums.Add(medium);
      return medium;
    }
    private NOSubject CreateSubject(string subjectName, string subjectCode, EducationManagementDbContext context)
    {
      NOSubject subject = new NOSubject();
      subject.SubjectName = subjectName;
      subject.SubjectCode = subjectCode;
      subject.FullMarks = 100;
      subject.ResultType = ResultTypeEnum.Percentage;
      subject.Status = StatusEnum.Active;
      subject.AuditFields.InsertedBy = "Automated";
      subject.AuditFields.InsertedDateTime = DateTime.Now;
      subject.AuditFields.LastUpdatedBy = "Automated";
      subject.AuditFields.LastUpdatedDateTime = DateTime.Now;
      context.Subjects.Add(subject);
      return subject;
    }
    private NOClass CreateClass(string className, string classCode, EducationManagementDbContext context)
    {
      NOClass noClass = new NOClass();
      noClass.ClassName = className;
      noClass.ClassCode = classCode;
      noClass.Status = StatusEnum.Active;
      noClass.AuditFields.InsertedBy = "Automated";
      noClass.AuditFields.InsertedDateTime = DateTime.Now;
      noClass.AuditFields.LastUpdatedBy = "Automated";
      noClass.AuditFields.LastUpdatedDateTime = DateTime.Now;
      context.Classes.Add(noClass);
      return noClass;
    }
    private void AddSubjectToClass(NOClass classOne, GroupEnum group, NOSubject subject, int fullMark, int passMark, EducationManagementDbContext context)
    {
      GroupSubject groupwiseSubject = new GroupSubject();
      groupwiseSubject.Class = classOne;
      groupwiseSubject.Group = group;
      groupwiseSubject.FullMarks = fullMark;
      groupwiseSubject.PassMarks = passMark;
      groupwiseSubject.Subject = subject;
      groupwiseSubject.Status = StatusEnum.Active;
      groupwiseSubject.AuditFields.InsertedBy = "Automated";
      groupwiseSubject.AuditFields.InsertedDateTime = DateTime.Now;
      groupwiseSubject.AuditFields.LastUpdatedBy = "Automated";
      groupwiseSubject.AuditFields.LastUpdatedDateTime = DateTime.Now;
      context.GroupSubjects.Add(groupwiseSubject);
    }
    private void CreateDesignation(string title, EmployeeTypeEnum employeeType, EducationManagementDbContext context)
    {
      NODesignation designation = new NODesignation();
      designation.Title = title;
      designation.EmployeeType = employeeType;
      designation.Status = StatusEnum.Active;
      designation.AuditFields.InsertedBy = "Automated";
      designation.AuditFields.InsertedDateTime = DateTime.Now;
      designation.AuditFields.LastUpdatedBy = "Automated";
      designation.AuditFields.LastUpdatedDateTime = DateTime.Now;
      context.Designations.Add(designation);
    }
    private void CreateJobType(string title, bool hasOverTime, EducationManagementDbContext context)
    {
      NOJobType jobType = new NOJobType();
      jobType.Title = title;
      jobType.HasOverTime = hasOverTime;
      jobType.Status = StatusEnum.Active;
      jobType.AuditFields.InsertedBy = "Automated";
      jobType.AuditFields.InsertedDateTime = DateTime.Now;
      jobType.AuditFields.LastUpdatedBy = "Automated";
      jobType.AuditFields.LastUpdatedDateTime = DateTime.Now;
      context.JobTypes.Add(jobType);
    }
    private void CreateEmployeeGrade(string title, decimal minSalary, decimal maxSalary, EducationManagementDbContext context)
    {
      NOGrade grade = new NOGrade();
      grade.GradeTitle = title;
      grade.MinSalary = minSalary;
      grade.MaxSalary = maxSalary;
      grade.Status = StatusEnum.Active;
      grade.AuditFields.InsertedBy = "Automated";
      grade.AuditFields.InsertedDateTime = DateTime.Now;
      grade.AuditFields.LastUpdatedBy = "Automated";
      grade.AuditFields.LastUpdatedDateTime = DateTime.Now;
      context.EmployeeGrades.Add(grade);
    }
    private void CreateLeaveType(string leaveName, int leavesPerYear, bool canEmployeesApplyBeyondTheCurrentLeaveBalance,
      bool willLeaveCarriedForward, int percentageOfLeaveCarriedForward, EducationManagementDbContext context)
    {
      NOLeaveType leaveType = new NOLeaveType();
      leaveType.LeaveName = leaveName;
      leaveType.LeavesPerYear = leavesPerYear;
      leaveType.CanEmployeesApplyBeyondTheCurrentLeaveBalance = canEmployeesApplyBeyondTheCurrentLeaveBalance;
      leaveType.WillLeaveCarriedForward = willLeaveCarriedForward;
      leaveType.PercentageOfLeaveCarriedForward = percentageOfLeaveCarriedForward;
      leaveType.Status = StatusEnum.Active;
      leaveType.AuditFields.InsertedBy = "Automated";
      leaveType.AuditFields.InsertedDateTime = DateTime.Now;
      leaveType.AuditFields.LastUpdatedBy = "Automated";
      leaveType.AuditFields.LastUpdatedDateTime = DateTime.Now;
      context.LeaveTypes.Add(leaveType);
    }
    private void CreatePublicHoliday(string holidayName, DateTime startDate, DateTime endDate, EducationManagementDbContext context)
    {
      NOPublicHoliday holiday = new NOPublicHoliday();
      holiday.HolidayName = holidayName;
      holiday.StartDate = startDate;
      holiday.EndDate = endDate;
      holiday.Status = StatusEnum.Active;
      holiday.AuditFields.InsertedBy = "Automated";
      holiday.AuditFields.InsertedDateTime = DateTime.Now;
      holiday.AuditFields.LastUpdatedBy = "Automated";
      holiday.AuditFields.LastUpdatedDateTime = DateTime.Now;
      context.PublicHolidays.Add(holiday);
    }
  }
}
