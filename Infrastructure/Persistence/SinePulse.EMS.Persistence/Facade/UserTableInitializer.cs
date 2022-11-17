using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Taxes;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Persistence.Facade
{
  public class UserTableInitializer : IUserTableInitializer
  {
    private readonly EmsDbContext _dbContext;
    private readonly UserManager<LoginUser> _userManager;

    public UserTableInitializer(EmsDbContext dbContext, UserManager<LoginUser> userManager)
    {
      _dbContext = dbContext;
      _userManager = userManager;
    }

    public void InitializeUserTable(AuditFields auditFields)
    {
      if (_userManager.Users.Any())
      {
        return;
      }

      var banglaMedium = CreateMedium("Bangla", "01", auditFields);
      var englishMedium = CreateMedium("English Version", "02", auditFields);

      var superAdmin = new LoginUser
      {
        UserName = "126265",
        NormalizedUserName = "126265",
        Email = "126265"
      };
      _userManager.CreateAsync(superAdmin, "Aplomb@R2").GetAwaiter().GetResult();
      _userManager.AddToRoleAsync(superAdmin, RoleNames.SuperAdmin).GetAwaiter().GetResult();

      var superAdminEmployee = new Staff
      {
        FullName = "Super Admin",
        EmployeeCode = "126265",
        AssociatedWith = AssociationType.Global,
        EmployeeType = EmployeeTypeEnum.Staff,
        LoginUser = superAdmin,
        AuditFields = Clone(auditFields)
      };
      _dbContext.Staffs.Add(superAdminEmployee);

      InitializeClassSubjects(auditFields);
      InitializePayrollComponent(auditFields);
      InitializaEmployeeGrade(auditFields);
      InitializeDesignation(auditFields);
      InitializeJobType(auditFields);
      InitializeLeaveType(auditFields);
      InitializeTaxRate(auditFields);

      _dbContext.SaveChanges();
    }

    private void InitializeTaxRate(AuditFields auditFields)
    {
      TaxRate taxRate = new TaxRate
      {
        EffectiveDate = new DateTime(2018,06,30),
        AuditFields = Clone(auditFields)
      };
      _dbContext.TaxRates.Add(taxRate);
      CreateTaxSlab(0, 250000, 0, taxRate);
      CreateTaxSlab(250001, 400000, 10, taxRate);
      CreateTaxSlab(400001, 500000, 15, taxRate);
      CreateTaxSlab(500001, 600000, 20, taxRate);
      CreateTaxSlab(600001, 3000000, 25, taxRate);
      CreateTaxSlab(3000001, null, 30, taxRate);
    }

    private void CreateTaxSlab(int limitFrom, int? limitTo, decimal percentage, TaxRate taxRate)
    {
      TaxRateDetail taxRateDetail = new TaxRateDetail
      {
        LimitFrom = limitFrom,
        LimitTo = limitTo,
        Percentage = percentage,
        TaxRate = taxRate
      };
      _dbContext.TaxRateDetails.Add(taxRateDetail);
    }
    private void InitializeLeaveType(AuditFields auditFields)
    {
      CreateLeaveType("Annual Leave", 15, false, true, 50, auditFields);
      CreateLeaveType("Casual Leave", 12, false, false, 0, auditFields);
      CreateLeaveType("Medical Leave", 12, true, false, 0, auditFields);
    }
    private void CreateLeaveType(string leaveName, int leavePerYear, bool canEmployeesApplyBeyondTheCurrentLeaveBalance,
      bool willLeaveCarriedForward, int percentageOfLeaveCarriedForward, AuditFields auditFields)
    {
      var leaveType = new LeaveType
      {
        LeaveName = leaveName,
        LeavesPerYear = leavePerYear,
        CanEmployeesApplyBeyondTheCurrentLeaveBalance = canEmployeesApplyBeyondTheCurrentLeaveBalance,
        WillLeaveCarriedForward = willLeaveCarriedForward,
        PercentageOfLeaveCarriedForward = percentageOfLeaveCarriedForward,
        Status = StatusEnum.Active,
        AuditFields = Clone(auditFields)
      };
      _dbContext.LeaveTypes.Add(leaveType);
    }
    private void InitializeJobType(AuditFields auditFields)
    {
      CreateJobType("Permanent", false, auditFields);
      CreateJobType("Part Time", true, auditFields);
    }
    private void CreateJobType(string title, bool hasOverTime, AuditFields auditFields)
    {
      var jobType = new JobType
      {
        JobTitle = title,
        HasOverTime = hasOverTime,
        Status = StatusEnum.Active,
        AuditFields = Clone(auditFields)
      };
      _dbContext.JobTypes.Add(jobType);
    }
    private void InitializeDesignation(AuditFields auditFields)
    {
      CrateDesignation("Asst. Teacher", EmployeeTypeEnum.Teacher, auditFields);
      CrateDesignation("Senior Teacher", EmployeeTypeEnum.Teacher, auditFields);
      CrateDesignation("Priciple", EmployeeTypeEnum.Teacher, auditFields);
      CrateDesignation("Executive", EmployeeTypeEnum.Staff, auditFields);
      CrateDesignation("Member", EmployeeTypeEnum.Committee, auditFields);
    }
    private void CrateDesignation(string designationName, EmployeeTypeEnum employeeType, AuditFields auditFields)
    {
      var designation = new Designation
      {
        DesignationName = designationName,
        EmployeeType = employeeType,
        Status = StatusEnum.Active,
        AuditFields = Clone(auditFields)
      };
      _dbContext.Designations.Add(designation);
    }
    private void InitializaEmployeeGrade(AuditFields auditFields)
    {
      CreateGrade(auditFields, "Grade 1", 5000, 8500);
    }
    private void InitializeClassSubjects(AuditFields auditFields)
    {
      var classOne = CreateClass("Class One", 10, auditFields);
      var classTwo = CreateClass("Class Two", 20, auditFields);
      var classThree = CreateClass("Class Three", 30, auditFields);
      var classFour = CreateClass("Class Four", 40, auditFields);
      var classFive = CreateClass("Class Five", 50, auditFields);
      var classSix = CreateClass("Class Six", 60, auditFields);
      var classSeven = CreateClass("Class Seven", 70, auditFields);
      var classEight = CreateClass("Class Eight", 80, auditFields);
      var classNine = CreateClass("Class Nine", 90, auditFields);
      var classTen = CreateClass("Class Ten", 100, auditFields);

      var bangla = CreateSubject("Bangla", 10, auditFields);
      var english = CreateSubject("English", 20, auditFields);
      var banglaFirst = CreateSubject("Bangla First", 30, auditFields);
      var banglaSecond = CreateSubject("Bangla Second", 40, auditFields);
      var englishFirst = CreateSubject("English First", 50, auditFields);
      var englishSecond = CreateSubject("English Second", 60, auditFields);
      var math = CreateSubject("Mathematics", 70, auditFields);
      var science = CreateSubject("General Science", 80, auditFields);
      var sociology = CreateSubject("Social Science", 90, auditFields);
      var physics = CreateSubject("Physics", 100, auditFields);
      var chemistry = CreateSubject("Chemistry", 110, auditFields);
      var biology = CreateSubject("Biology", 120, auditFields);
      var higherMath = CreateSubject("Higher Mathematics", 130, auditFields);
      var accounting = CreateSubject("Accounting", 140, auditFields);
      var marketing = CreateSubject("Marketing", 150, auditFields);
      var businessLaw = CreateSubject("Business Law", 160, auditFields);
      var agriculture = CreateSubject("Agriculture", 170, auditFields);
      var homeEconomics = CreateSubject("Home Economics", 180, auditFields);
      var islam = CreateSubject("Islam", 190, auditFields);
      var hinduism = CreateSubject("Hinduism", 200, auditFields);
      var christianity = CreateSubject("Christianity", 210, auditFields);
      var buddhism = CreateSubject("Buddhism", 220, auditFields);
     
      var classOneBangla = CreateClassSubject(classOne, bangla, auditFields, false);
      var classOneEnglish = CreateClassSubject(classOne, english, auditFields,false);
      var classOneMath = CreateClassSubject(classOne, math, auditFields,false);
      var classTwoBangla = CreateClassSubject(classTwo, bangla, auditFields, false);
      var classTwoEnglish = CreateClassSubject(classTwo, english, auditFields, false);
      var classTwoMath = CreateClassSubject(classTwo, math, auditFields, false);

      var classThreeBangla = CreateClassSubject(classThree, bangla, auditFields, false);
      var classThreeEnglish = CreateClassSubject(classThree, english, auditFields, false);
      var classThreeMath = CreateClassSubject(classThree, math, auditFields, false);
      var classThreeIslam = CreateClassSubject(classThree, islam, auditFields, true);
      var classThreeHinduism = CreateClassSubject(classThree, hinduism, auditFields, true);
      var classThreeChristianity = CreateClassSubject(classThree, christianity, auditFields, true);
      var classThreeBuddhism = CreateClassSubject(classThree, buddhism, auditFields, true);
      var classThreeSociology = CreateClassSubject(classThree, sociology, auditFields, false);
      var classThreeScience = CreateClassSubject(classThree, science, auditFields, false);

      var classFourBangla = CreateClassSubject(classFour, bangla, auditFields, false);
      var classFourEnglish = CreateClassSubject(classFour, english, auditFields, false);
      var classFourMath = CreateClassSubject(classFour, math, auditFields, false);
      var classFourIslam = CreateClassSubject(classFour, islam, auditFields, true);
      var classFourHinduism = CreateClassSubject(classFour, hinduism, auditFields, true);
      var classFourChristianity = CreateClassSubject(classFour, christianity, auditFields, true);
      var classFourBuddhism = CreateClassSubject(classFour, buddhism, auditFields, true);
      var classFourSociology = CreateClassSubject(classFour, sociology, auditFields, false);
      var classFourScience = CreateClassSubject(classFour, science, auditFields, false);

      var classFiveBangla = CreateClassSubject(classFive, bangla, auditFields, false);
      var classFiveEnglish = CreateClassSubject(classFive, english, auditFields, false);
      var classFiveMath = CreateClassSubject(classFive, math, auditFields, false);
      var classFiveIslam = CreateClassSubject(classFive, islam, auditFields, true);
      var classFiveHinduism = CreateClassSubject(classFive, hinduism, auditFields, true);
      var classFiveChristianity = CreateClassSubject(classFive, christianity, auditFields, true);
      var classFiveBuddhism = CreateClassSubject(classFive, buddhism, auditFields, true);
      var classFiveSociology = CreateClassSubject(classFive, sociology, auditFields, false);
      var classFiveScience = CreateClassSubject(classFive, science, auditFields, false);

      var classSixBanglaFirst = CreateClassSubject(classSix, banglaFirst, auditFields, false);
      var classSixBanglaSecond = CreateClassSubject(classSix, banglaSecond, auditFields, false);
      var classSixEnglishFirst = CreateClassSubject(classSix, englishFirst, auditFields, false);
      var classSixEnglishSecond = CreateClassSubject(classSix, englishSecond, auditFields, false);
      var classSixMath = CreateClassSubject(classSix, math, auditFields, false);
      var classSixIslam = CreateClassSubject(classSix, islam, auditFields, true);
      var classSixHinduism = CreateClassSubject(classSix, hinduism, auditFields, true);
      var classSixChristianity = CreateClassSubject(classSix, christianity, auditFields, true);
      var classSixBuddhism = CreateClassSubject(classSix, buddhism, auditFields, true);
      var classSixSociology = CreateClassSubject(classSix, sociology, auditFields, false);
      var classSixScience = CreateClassSubject(classSix, science, auditFields, false);
      var classSixAgriculture = CreateClassSubject(classSix, agriculture, auditFields, true);
      var classSixHomeEconomics = CreateClassSubject(classSix, homeEconomics, auditFields, true);

      var classSevenBanglaFirst = CreateClassSubject(classSeven, banglaFirst, auditFields, false);
      var classSevenBanglaSecond = CreateClassSubject(classSeven, banglaSecond, auditFields, false);
      var classSevenEnglishFirst = CreateClassSubject(classSeven, englishFirst, auditFields, false);
      var classSevenEnglishSecond = CreateClassSubject(classSeven, englishSecond, auditFields, false);
      var classSevenMath = CreateClassSubject(classSeven, math, auditFields, false);
      var classSevenIslam = CreateClassSubject(classSeven, islam, auditFields, true);
      var classSevenHinduism = CreateClassSubject(classSeven, hinduism, auditFields, true);
      var classSevenChristianity = CreateClassSubject(classSeven, christianity, auditFields, true);
      var classSevenBuddhism = CreateClassSubject(classSeven, buddhism, auditFields, true);
      var classSevenSociology = CreateClassSubject(classSeven, sociology, auditFields, false);
      var classSevenScience = CreateClassSubject(classSeven, science, auditFields, false);
      var classSevenAgriculture = CreateClassSubject(classSeven, agriculture, auditFields, true);
      var classSevenHomeEconomics = CreateClassSubject(classSeven, homeEconomics, auditFields, true);

      var classEightBanglaFirst = CreateClassSubject(classEight, banglaFirst, auditFields, false);
      var classEightBanglaSecond = CreateClassSubject(classEight, banglaSecond, auditFields, false);
      var classEightEnglishFirst = CreateClassSubject(classEight, englishFirst, auditFields, false);
      var classEightEnglishSecond = CreateClassSubject(classEight, englishSecond, auditFields, false);
      var classEightMath = CreateClassSubject(classEight, math, auditFields, false);
      var classEightIslam = CreateClassSubject(classEight, islam, auditFields, true);
      var classEightHinduism = CreateClassSubject(classEight, hinduism, auditFields, true);
      var classEightChristianity = CreateClassSubject(classEight, christianity, auditFields, true);
      var classEightBuddhism = CreateClassSubject(classEight, buddhism, auditFields, true);
      var classEightSociology = CreateClassSubject(classEight, sociology, auditFields, false);
      var classEightScience = CreateClassSubject(classEight, science, auditFields, false);
      var classEightAgriculture = CreateClassSubject(classEight, agriculture, auditFields, true);
      var classEightHomeEconomics = CreateClassSubject(classEight, homeEconomics, auditFields, true);

      var classNineScienceBanglaFirst = CreateClassSubject(classNine, banglaFirst, auditFields, false, GroupEnum.Science);
      var classNineScienceBanglaSecond = CreateClassSubject(classNine, banglaSecond, auditFields, false, GroupEnum.Science);
      var classNineScienceEnglishFirst = CreateClassSubject(classNine, englishFirst, auditFields, false, GroupEnum.Science);
      var classNineScienceEnglishSecond = CreateClassSubject(classNine, englishSecond, auditFields, false, GroupEnum.Science);
      var classNineScienceMath = CreateClassSubject(classNine, math, auditFields, false, GroupEnum.Science);
      var classNineSciencePhysics = CreateClassSubject(classNine, physics, auditFields, false, GroupEnum.Science);
      var classNineScienceChemistry = CreateClassSubject(classNine, chemistry, auditFields, false, GroupEnum.Science);
      var classNineScienceBiology = CreateClassSubject(classNine, biology, auditFields, true, GroupEnum.Science);
      var classNineScienceHigherMath = CreateClassSubject(classNine, higherMath, auditFields, true, GroupEnum.Science);
      var classNineScienceAgriculture = CreateClassSubject(classNine, agriculture, auditFields, true, GroupEnum.Science);
      var classNineScienceIslam = CreateClassSubject(classNine, islam, auditFields, true, GroupEnum.Science);
      var classNineScienceHinduism = CreateClassSubject(classNine, hinduism, auditFields, true, GroupEnum.Science);
      var classNineScienceChristianity = CreateClassSubject(classNine, christianity, auditFields, true, GroupEnum.Science);
      var classNineScienceBuddhism = CreateClassSubject(classNine, buddhism, auditFields, true, GroupEnum.Science);

      var classNineArtsBanglaFirst = CreateClassSubject(classNine, banglaFirst, auditFields, false, GroupEnum.Arts);
      var classNineArtsBanglaSecond = CreateClassSubject(classNine, banglaSecond, auditFields, false, GroupEnum.Arts);
      var classNineArtsEnglishFirst = CreateClassSubject(classNine, englishFirst, auditFields, false, GroupEnum.Arts);
      var classNineArtsEnglishSecond = CreateClassSubject(classNine, englishSecond, auditFields, false, GroupEnum.Arts);
      var classNineArtsMath = CreateClassSubject(classNine, math, auditFields, false, GroupEnum.Arts);
      var classNineArtsSociology = CreateClassSubject(classNine, sociology, auditFields, false, GroupEnum.Arts);
      var classNineArtsHomeEconomics = CreateClassSubject(classNine, homeEconomics, auditFields, false, GroupEnum.Arts);
      var classNineArtsScience = CreateClassSubject(classNine, science, auditFields, true, GroupEnum.Arts);
      var classNineArtsIslam = CreateClassSubject(classNine, islam, auditFields, true, GroupEnum.Arts);
      var classNineArtsHinduism = CreateClassSubject(classNine, hinduism, auditFields, true, GroupEnum.Arts);
      var classNineArtsChristianity = CreateClassSubject(classNine, christianity, auditFields, true, GroupEnum.Arts);
      var classNineArtsBuddhism = CreateClassSubject(classNine, buddhism, auditFields, true, GroupEnum.Arts);

      var classNineCommerceBanglaFirst = CreateClassSubject(classNine, banglaFirst, auditFields, false, GroupEnum.Commerce);
      var classNineCommerceBanglaSecond = CreateClassSubject(classNine, banglaSecond, auditFields, false, GroupEnum.Commerce);
      var classNineCommerceEnglishFirst = CreateClassSubject(classNine, englishFirst, auditFields, false, GroupEnum.Commerce);
      var classNineCommerceEnglishSecond = CreateClassSubject(classNine, englishSecond, auditFields, false, GroupEnum.Commerce);
      var classNineCommerceMath = CreateClassSubject(classNine, math, auditFields, false, GroupEnum.Commerce);
      var classNineCommerceAccounting = CreateClassSubject(classNine, accounting, auditFields, false, GroupEnum.Commerce);
      var classNineCommerceMarketing = CreateClassSubject(classNine, marketing, auditFields, false, GroupEnum.Commerce);
      var classNineCommerceBusinessLaw = CreateClassSubject(classNine, businessLaw, auditFields,false, GroupEnum.Commerce);
      var classNineCommerceScience = CreateClassSubject(classNine, science, auditFields, true, GroupEnum.Commerce);
      var classNineCommerceIslam = CreateClassSubject(classNine, islam, auditFields, true, GroupEnum.Commerce);
      var classNineCommerceHinduism = CreateClassSubject(classNine, hinduism, auditFields, true, GroupEnum.Commerce);
      var classNineCommerceChristianity = CreateClassSubject(classNine, christianity, auditFields, true, GroupEnum.Commerce);
      var classNineCommerceBuddhism = CreateClassSubject(classNine, buddhism, auditFields, true, GroupEnum.Commerce);

      var classTenScienceBanglaFirst = CreateClassSubject(classTen, banglaFirst, auditFields, false, GroupEnum.Science);
      var classTenScienceBanglaSecond = CreateClassSubject(classTen, banglaSecond, auditFields, false, GroupEnum.Science);
      var classTenScienceEnglishFirst = CreateClassSubject(classTen, englishFirst, auditFields, false, GroupEnum.Science);
      var classTenScienceEnglishSecond = CreateClassSubject(classTen, englishSecond, auditFields, false, GroupEnum.Science);
      var classTenScienceMath = CreateClassSubject(classTen, math, auditFields, false, GroupEnum.Science);
      var classTenSciencePhysics = CreateClassSubject(classTen, physics, auditFields, false, GroupEnum.Science);
      var classTenScienceChemistry = CreateClassSubject(classTen, chemistry, auditFields, false, GroupEnum.Science);
      var classTenScienceBiology = CreateClassSubject(classTen, biology, auditFields, true, GroupEnum.Science);
      var classTenScienceHigherMath = CreateClassSubject(classTen, higherMath, auditFields, true, GroupEnum.Science);
      var classTenScienceAgriculture = CreateClassSubject(classTen, agriculture, auditFields, true, GroupEnum.Science);
      var classTenScienceIslam = CreateClassSubject(classTen, islam, auditFields, true, GroupEnum.Science);
      var classTenScienceHinduism = CreateClassSubject(classTen, hinduism, auditFields, true, GroupEnum.Science);
      var classTenScienceChristianity = CreateClassSubject(classTen, christianity, auditFields, true, GroupEnum.Science);
      var classTenScienceBuddhism = CreateClassSubject(classTen, buddhism, auditFields, true, GroupEnum.Science);

      var classTenArtsBanglaFirst = CreateClassSubject(classTen, banglaFirst, auditFields, false, GroupEnum.Arts);
      var classTenArtsBanglaSecond = CreateClassSubject(classTen, banglaSecond, auditFields, false, GroupEnum.Arts);
      var classTenArtsEnglishFirst = CreateClassSubject(classTen, englishFirst, auditFields, false, GroupEnum.Arts);
      var classTenArtsEnglishSecond = CreateClassSubject(classTen, englishSecond, auditFields, false, GroupEnum.Arts);
      var classTenArtsMath = CreateClassSubject(classTen, math, auditFields, false, GroupEnum.Arts);
      var classTenArtsSociology = CreateClassSubject(classTen, sociology, auditFields, false, GroupEnum.Arts);
      var classTenArtsHomeEconomics = CreateClassSubject(classTen, homeEconomics, auditFields, false, GroupEnum.Arts);
      var classTenArtsScience = CreateClassSubject(classTen, science, auditFields, true, GroupEnum.Arts);
      var classTenArtsIslam = CreateClassSubject(classTen, islam, auditFields, true, GroupEnum.Arts);
      var classTenArtsHinduism = CreateClassSubject(classTen, hinduism, auditFields, true, GroupEnum.Arts);
      var classTenArtsChristianity = CreateClassSubject(classTen, christianity, auditFields, true, GroupEnum.Arts);
      var classTenArtsBuddhism = CreateClassSubject(classTen, buddhism, auditFields, true, GroupEnum.Arts);

      var classTenCommerceBanglaFirst = CreateClassSubject(classTen, banglaFirst, auditFields, false, GroupEnum.Commerce);
      var classTenCommerceBanglaSecond = CreateClassSubject(classTen, banglaSecond, auditFields, false, GroupEnum.Commerce);
      var classTenCommerceEnglishFirst = CreateClassSubject(classTen, englishFirst, auditFields, false, GroupEnum.Commerce);
      var classTenCommerceEnglishSecond = CreateClassSubject(classTen, englishSecond, auditFields, false, GroupEnum.Commerce);
      var classTenCommerceMath = CreateClassSubject(classTen, math, auditFields, false, GroupEnum.Commerce);
      var classTenCommerceAccounting = CreateClassSubject(classTen, accounting, auditFields, false, GroupEnum.Commerce);
      var classTenCommerceMarketing = CreateClassSubject(classTen, marketing, auditFields, false, GroupEnum.Commerce);
      var classTenCommerceBusinessLaw = CreateClassSubject(classTen, businessLaw, auditFields, false, GroupEnum.Commerce);
      var classTenCommerceScience = CreateClassSubject(classTen, science, auditFields, true, GroupEnum.Commerce);
      var classTenCommerceIslam = CreateClassSubject(classTen, islam, auditFields, true, GroupEnum.Commerce);
      var classTenCommerceHinduism = CreateClassSubject(classTen, hinduism, auditFields, true, GroupEnum.Commerce);
      var classTenCommerceChristianity = CreateClassSubject(classTen, christianity, auditFields, true, GroupEnum.Commerce);
      var classTenCommerceBuddhism = CreateClassSubject(classTen, buddhism, auditFields, true, GroupEnum.Commerce);
    }
    private void InitializePayrollComponent(AuditFields auditFields)
    {
      var basic = CreateSalaryComponentType("Basic", auditFields);
      var allowance = CreateSalaryComponentType("Allowance", auditFields);
      var hourly = CreateSalaryComponentType("Hourly", auditFields);

      var basicSalary = CreateSalaryComponent("Basic Salary", basic, auditFields);
      var carAllowance = CreateSalaryComponent("House Rent Allowance", allowance, auditFields);
      var telephoneAllowance = CreateSalaryComponent("Medical Allowance", allowance, auditFields);
      var overtimeHourlyPay = CreateSalaryComponent("Conveyance Allowance", allowance, auditFields);
      var fixedAllowance = CreateSalaryComponent("Fixed Allowance", allowance, auditFields);

      CreateOtherComponent("Tax Deduction", auditFields);
      CreateOtherComponent("Other Deduction", auditFields);
    }
    private Grade CreateGrade(AuditFields auditFields, string gradeTitle, int minSalary, int maxSalary)
    {
      var superAdminGrade = new Grade
      {
        GradeTitle = gradeTitle,
        MinSalary = minSalary,
        MaxSalary = maxSalary,
        Status = StatusEnum.Active,
        AuditFields = Clone(auditFields)
      };
      _dbContext.Grades.Add(superAdminGrade);
      return superAdminGrade;
    }
    private Medium CreateMedium(string mediumName, string mediumCode, AuditFields auditFields)
    {
      var medium = new Medium
      {
        MediumName = mediumName,
        MediumCode = mediumCode,
        Status = StatusEnum.Active,
        AuditFields = Clone(auditFields)
      };
      _dbContext.Mediums.Add(medium);
      return medium;
    }
    private ClassSubject CreateClassSubject(Class @class, Subject subject, AuditFields auditFields, bool isOptional, GroupEnum @group = GroupEnum.NoGroup)
    {
      var classSubject = new ClassSubject
      {
        Class = @class,
        Subject = subject,
        Group = group,
        IsOptional = isOptional,
        AuditFields = Clone(auditFields),
      };
      _dbContext.ClassSubjects.Add(classSubject);
      return classSubject;
    }
    private Subject CreateSubject(string subjectName, int subjectCode, AuditFields auditFields)
    {
      var subject = new Subject
      {
        SubjectName = subjectName,
        SubjectCode = subjectCode,
        Status = StatusEnum.Active,
        AuditFields = Clone(auditFields)
      };
      _dbContext.Subjects.Add(subject);
      return subject;
    }
    private Class CreateClass(string className, int classCode, AuditFields auditFields)
    {
      var @class = new Class
      {
        ClassName = className,
        ClassCode = classCode,
        Status = StatusEnum.Active,
        AuditFields = Clone(auditFields)
      };
      _dbContext.Classes.Add(@class);
      return @class;
    }
    private SalaryComponentType CreateSalaryComponentType(string componentType, AuditFields auditFields)
    {
      var salaryComponentType = new SalaryComponentType
      {
        ComponentType = componentType,
        Status = StatusEnum.Active,
        AuditFields = Clone(auditFields)
      };
      _dbContext.SalaryComponentTypes.Add(salaryComponentType);
      return salaryComponentType;
    }
    private SalaryComponent CreateSalaryComponent(string componentName, SalaryComponentType componentType, AuditFields auditFields)
    {
      var salaryComponent = new SalaryComponent
      {
        ComponentName = componentName,
        IncreaseDecrease = IncreaseDecreaseEnum.Increase,
        ComponentType = componentType,
        AuditFields = Clone(auditFields)
      };
      _dbContext.SalaryComponents.Add(salaryComponent);
      return salaryComponent;
    }
    private OtherComponent CreateOtherComponent(string componentName, AuditFields auditFields)
    {
      var otherComponent = new OtherComponent
      {
        ComponentName = componentName,
        IncreaseDecrease = IncreaseDecreaseEnum.Decrease,
        AuditFields = Clone(auditFields)
      };
      _dbContext.OtherComponents.Add(otherComponent);
      return otherComponent;
    }
    private AuditFields Clone(AuditFields auditFields)
    {
      return new AuditFields
      {
        InsertedBy = auditFields.InsertedBy,
        InsertedDateTime = auditFields.InsertedDateTime,
        LastUpdatedBy = auditFields.LastUpdatedBy,
        LastUpdatedDateTime = auditFields.LastUpdatedDateTime
      };
    }
  }
}