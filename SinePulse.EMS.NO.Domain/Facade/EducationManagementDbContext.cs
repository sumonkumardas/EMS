using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Accounts;
using SinePulse.EMS.NO.Domain.Attendance;
using SinePulse.EMS.NO.Domain.Banks;
using SinePulse.EMS.NO.Domain.Designations;
using SinePulse.EMS.NO.Domain.Employees;
using SinePulse.EMS.NO.Domain.Examinations;
using SinePulse.EMS.NO.Domain.Features;
using SinePulse.EMS.NO.Domain.Holidays;
using SinePulse.EMS.NO.Domain.Leaves;
using SinePulse.EMS.NO.Domain.Routines;
using SinePulse.EMS.NO.Domain.Shared;
using SinePulse.EMS.NO.Domain.Students;
using SinePulse.EMS.NO.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Facade
{
  public class EducationManagementDbContext : DbContext
  {
    public EducationManagementDbContext() : base("name=CommandModelDatabase")
    {
    }
    public DbSet<NOAddress> Addresses { get; set; }
    public DbSet<NOInstitute> Institutes { get; set; }
    public DbSet<NOBranch> Branches { get; set; }
    public DbSet<NOSession> Sessions { get; set; }
    public DbSet<NOShift> Shifts { get; set; }
    public DbSet<NOMedium> Mediums { get; set; }
    public DbSet<NOSubject> Subjects { get; set; }
    public DbSet<NOClass> Classes { get; set; }
    public DbSet<NOSection> Sections { get; set; }
    public DbSet<BranchMedium> BranchMediums { get; set; }
    public DbSet<GroupSubject> GroupSubjects { get; set; }
    public DbSet<NODesignation> Designations { get; set; }
    public DbSet<NOJobType> JobTypes { get; set; }
    public DbSet<NOWorkingShift> WorkingShifts { get; set; }
    public DbSet<NOGrade> EmployeeGrades { get; set; }
    public DbSet<NOEmployee> Employees { get; set; }
    public DbSet<NOEmployeePersonalInfo> EmployeePersonalInfos { get; set; }
    public DbSet<NOEducationalQualification> EducationalQualifications { get; set; }
    public DbSet<NOBankInfo> BankInfos { get; set; }
    public DbSet<NOClassRoutine> Routines { get; set; }
    public DbSet<NOLeaveType> LeaveTypes { get; set; }
    public DbSet<NOPublicHoliday> PublicHolidays { get; set; }
    public DbSet<NOEmployeeLeave> EmployeeLeaves { get; set; }
    public DbSet<NOAccountType> AccountTypes { get; set; }
    public DbSet<NOAccountTransactionType> AccountTransactionTypes { get; set; }
    public DbSet<NOAccountHead> AccountHeads { get; set; }
    public DbSet<NOVoucherAccountTypeRelationship> VoucherAccountTypeRelationships { get; set; }
    public DbSet<NOAutoPostingConfig> AutoPostingConfigs { get; set; }
    public DbSet<NOBankAccountHead> BankAccountHeads { get; set; }
    public DbSet<NOAcademicFee> AcademicFees { get; set; }
    public DbSet<NOTerm> ExamTerms { get; set; }
    public DbSet<NOExamType> ExamTypes { get; set; }
    public DbSet<NOClassTest> ClassTests { get; set; }
    public DbSet<NOExamRoutine> ExamRoutines { get; set; }
    public DbSet<NORoom> Rooms { get; set; }
    public DbSet<NOBreakTime> BreakTimes { get; set; }
    public DbSet<NORole> Roles { get; set; }
    public DbSet<NOLoginUser> LoginUsers { get; set; }
    public DbSet<NOUserRoles> UserRoles { get; set; }
    public DbSet<NOUserLogins> UserLogins { get; set; }
    public DbSet<NOUserClaims> UserClaims { get; set; }
    public DbSet<NOFeatureType> FeatureTypes { get; set; }
    public DbSet<NOFeature> Features { get; set; }
    public DbSet<NORoleFeatures> RoleFeatures { get; set; }
    public DbSet<NOStudent> Students { get; set; }
    public DbSet<NOStudentAttendance> StudentAttendanceData { get; set; }
    public DbSet<NOEmployeeAttendance> EmployeeAttendanceData { get; set; }
    public DbSet<NOContactInfo> ContactInfos { get; set; }
    public DbSet<NOContactPerson> ContactPersons { get; set; }
    public DbSet<NOStudentSection> StudentSections { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
       //Database.SetInitializer(new CreateDatabaseIfNotExists<EducationManagementDbContext>());
      //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EducationManagementDbContext>());
      //Database.SetInitializer<EducationManagementDbContext>(null);

      Database.SetInitializer(new SampleDbInitializer());
    }
  }
}
