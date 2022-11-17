using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Attendance;
using SinePulse.EMS.Domain.Banks;
using SinePulse.EMS.Domain.Billing;
using SinePulse.EMS.Domain.Calendars;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.Holidays;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Domain.ManagingCommittee;
using SinePulse.EMS.Domain.Notification;
using SinePulse.EMS.Domain.PaymentGateway;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Domain.Routines;
using SinePulse.EMS.Domain.ScheduleJobManagement;
using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Domain.Taxes;
using SinePulse.EMS.Domain.Transactions;
using SinePulse.EMS.Domain.UserManagement;

namespace SinePulse.EMS.Persistence.Facade
{
  public class EmsDbContext : IdentityDbContext<LoginUser, Role, string>
  {
    private readonly ISchemaInitializer _schemaInitializer;

    public EmsDbContext() 
    {
    }
    public EmsDbContext(string connectionString, ISchemaInitializer schemaInitializer) : base(GetOptions(connectionString))
    {
      _schemaInitializer = schemaInitializer;
    }
    public EmsDbContext(DbContextOptions<EmsDbContext> options, ISchemaInitializer schemaInitializer)
            : base(options)
    {
      _schemaInitializer = schemaInitializer;
    }
    private static DbContextOptions GetOptions(string connectionString)
    {
      return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
    }
    public DbSet<Institute> Institutes { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<BranchMedium> BranchMediums { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Medium> Mediums { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Calendar> Calendars { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<WorkingShift> WorkingShifts { get; set; }
    public DbSet<PublicHoliday> PublicHolidays { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<ClassRoutine> ClassRoutines { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<ContactPerson> ContactPersons { get; set; }
    public DbSet<StudentSection> StudentSections { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Designation> Designations { get; set; }
    public DbSet<ClassSubject> ClassSubjects { get; set; }
    public DbSet<SubjectChoice> SubjectChoices { get; set; }
    public DbSet<ExamTerm> ExamTerms { get; set; }
    public DbSet<ExamConfiguration> ExamConfigurations { get; set; }
    public DbSet<ClassTest> ClassTests { get; set; }
    public DbSet<SeatPlan> SeatPlans { get; set; }
    public DbSet<ClassTestMark> Marks { get; set; }
    public DbSet<ResultGrade> ResultGrades { get; set; }
    public DbSet<ResultSheet> ResultSheets { get; set; }
    public DbSet<ResultSheetEntry> ResultSheetEntries { get; set; }
    public DbSet<EmployeePersonalInfo> EmployeePersonalInfos { get; set; }
    public DbSet<JobType> JobTypes { get; set; }
    public DbSet<EducationalQualification> EducationalQualifications { get; set; }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<EmployeeLeave> EmployeeLeave { get; set; }
    public DbSet<BreakTime> BreakTimes { get; set; }
    public DbSet<AccountHead> AccountHeads { get; set; }
    public DbSet<AcademicFee> AcademicFees { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }
    public DbSet<FeatureType> FeatureTypes { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<RoleFeature> RoleFeatures { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<AccountTransactionType> AccountTransactionTypes { get; set; }
    public DbSet<BranchMediumAccountHead> BranchMediumAccountsHeads { get; set; }
    public DbSet<AutoPostingConfiguration> AutoPostingConfigurations { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionEntry> TransactionEntries { get; set; }
    public DbSet<TransactionNoTrackingData> TransactionNoTrackingDatas { get; set; }
    public DbSet<DailyBalance> DailyBalances { get; set; }
    public DbSet<CommitteeMember> CommitteeMembers { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<BankAccountHead> BankAccountHeads { get; set; }
    public DbSet<BankBranch> BankBranches { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<MidNightTaskTrackingInfo> MidNightTaskTrackingInfos { get; set; }
    public DbSet<StudentWaiver> StudentWaivers { get; set; }
    public DbSet<TermTest> TermTests { get; set; }
    public DbSet<FeeCollection> FeeCollections { get; set; }
    public DbSet<TermTestMark> TermTestMarks { get; set; }
    public DbSet<NotificationConfiguration> NotificationConfigurations { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<ServiceCharge> ServiceCharges { get; set; }
    public DbSet<BillingData> BillingData { get; set; }
    public DbSet<PendingBillInfo> PendingBillInfos { get; set; }
    public DbSet<SslPaymentGateway> SslPaymentGateways { get; set; }
    public DbSet<SalaryComponentType> SalaryComponentTypes { get; set; }
    public DbSet<PayrollComponent> PayrollComponents { get; set; }
    public DbSet<SalaryComponent> SalaryComponents { get; set; }
    public DbSet<OtherComponent> OtherComponents { get; set; }
    public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
    public DbSet<SalarySheet> SalarySheets { get; set; }
    public DbSet<SalarySheetDetail> SalarySheetDetail { get; set; }
    public DbSet<CommitteeMemberPersonalInfo> CommitteeMemberPersonalInfos { get; set; }
    public DbSet<TaxRate> TaxRates { get; set; }
    public DbSet<TaxRateDetail> TaxRateDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      _schemaInitializer.InitializeSchema(modelBuilder);
//
//      var dbInitializer = new DbInitializer();
//      dbInitializer.Seed(modelBuilder);
    }
  }
}