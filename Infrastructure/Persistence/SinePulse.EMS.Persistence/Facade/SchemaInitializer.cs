using System;
using Microsoft.AspNetCore.Identity;
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

namespace SinePulse.EMS.Persistence.Facade
{
  public class SchemaInitializer : ISchemaInitializer
  {
    public void InitializeSchema(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Institute>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Branch>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<BranchMedium>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Section>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Medium>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Session>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Session>().HasIndex(p => p.StartDate);
      modelBuilder.Entity<Session>().HasIndex(p => p.EndDate);
      modelBuilder.Entity<Session>().HasIndex(p => p.IsSessionClosing);
      modelBuilder.Entity<Session>().HasIndex(p => p.IsSessionClosed);
      modelBuilder.Entity<Session>().HasIndex(p => p.Status);
      modelBuilder.Entity<Session>()
        .HasOne(x => x.BranchMedium)
        .WithMany(x => x.Sessions)
        .HasForeignKey(x => x.BranchMediumId);
      modelBuilder.Entity<Class>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Shift>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<PublicHoliday>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Room>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Subject>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Calendar>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ExamTerm>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ExamConfiguration>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ClassTest>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<SeatPlan>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<SeatPlan>()
        .HasOne(x => x.Room)
        .WithMany()
        .HasForeignKey(x => x.RoomId);
      modelBuilder.Entity<SeatPlan>()
        .HasOne(x => x.Test)
        .WithMany()
        .HasForeignKey(x => x.TestId);
      modelBuilder.Entity<ClassTestMark>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ResultGrade>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ResultSheet>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ResultSheet>()
        .HasOne(x => x.Term)
        .WithMany()
        .HasForeignKey(x => x.TermId);
      modelBuilder.Entity<ResultSheet>()
        .HasOne(x => x.StudentSection)
        .WithMany()
        .HasForeignKey(x => x.StudentSectionId);
      modelBuilder.Entity<ResultSheetEntry>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ResultSheet>()
        .HasMany(x => x.ResultSheetEntries)
        .WithOne(x => x.ResultSheet);
      modelBuilder.Entity<Employee>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Grade>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<WorkingShift>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Staff>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Teacher>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ClassRoutine>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Student>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ContactPerson>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<StudentSection>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Address>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Designation>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ClassSubject>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<SubjectChoice>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<JobType>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<LeaveType>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<EmployeePersonalInfo>().OwnsOne(p => p.AuditFields);

      modelBuilder.Entity<Employee>()
        .HasOne(c => c.ReportTo)
        .WithMany()
        .HasForeignKey(c => c.ReportToId);

      modelBuilder.Entity<Branch>()
        .HasOne(p => p.Institute)
        .WithMany(b => b.Branches);

      modelBuilder.Entity<BranchMedium>()
        .HasOne(p => p.Branch)
        .WithMany(b => b.Mediums);

      modelBuilder.Entity<RoleFeature>()
        .HasOne(p => p.Role)
        .WithMany(b => b.Features);

      modelBuilder.Entity<EducationalQualification>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<EmployeeLeave>().OwnsOne(p => p.AuditFields);

      modelBuilder.Entity<Section>()
        .HasOne(x => x.Class)
        .WithMany()
        .HasForeignKey(x => x.ClassId);
      modelBuilder.Entity<Section>()
        .HasOne(x => x.BranchMedium)
        .WithMany(x => x.Sections)
        .HasForeignKey(x => x.BranchMediumId);

      modelBuilder.Entity<BreakTime>().OwnsOne(p => p.AuditFields);

      modelBuilder.Entity<AccountHead>()
        .HasOne(s => s.ParentHead);
      modelBuilder.Entity<AccountHead>()
        .HasOne(s => s.AccountType);

      modelBuilder.Entity<AccountHead>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<AcademicFee>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ContactInfo>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Student>()
        .HasOne(s => s.PresentAddress);
      modelBuilder.Entity<Student>()
        .HasOne(s => s.PermanentAddress);
      modelBuilder.Entity<ContactInfo>()
        .HasOne(s => s.PresentAddress);
      modelBuilder.Entity<ContactInfo>()
        .HasOne(s => s.PermanentAddress);
      modelBuilder.Entity<AccountType>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<AccountTransactionType>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<BranchMediumAccountHead>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Student>()
        .HasOne(s => s.BranchMedium);
      modelBuilder.Entity<Student>()
        .HasOne(s => s.Session);
      modelBuilder.Entity<StudentSection>()
        .HasOne(x => x.Student)
        .WithMany()
        .HasForeignKey(x => x.StudentId);
      modelBuilder.Entity<StudentSection>()
        .HasOne(x => x.Class)
        .WithMany()
        .HasForeignKey(x => x.ClassId);
      modelBuilder.Entity<AutoPostingConfiguration>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Transaction>().OwnsOne(x => x.AuditFields);
      modelBuilder.Entity<Transaction>().HasAlternateKey(x => x.TransactionNo);
      modelBuilder.Entity<Transaction>()
        .HasMany(x => x.TransactionEntries)
        .WithOne(x => x.Transaction);
      modelBuilder.Entity<TransactionEntry>().OwnsOne(x => x.AuditFields);
      modelBuilder.Entity<TransactionNoTrackingData>().OwnsOne(x => x.AuditFields);
      modelBuilder.Entity<TransactionNoTrackingData>().HasOne(x => x.BranchMedium)
        .WithMany()
        .HasForeignKey(x => x.BranchMediumId);
      modelBuilder.Entity<TransactionNoTrackingData>()
        .HasAlternateKey(x => new {x.TransactionPrefix, x.BranchMediumId});
      modelBuilder.Entity<DailyBalance>().HasOne(x => x.AccountHead)
        .WithMany()
        .HasForeignKey(x => x.AccountHeadId);
      modelBuilder.Entity<DailyBalance>()
        .HasAlternateKey(x => new {x.Date, x.AccountHeadId});
      modelBuilder.Entity<Bank>().OwnsOne(x => x.AuditFields);
      modelBuilder.Entity<CommitteeMember>().OwnsOne(x => x.AuditFields);
      modelBuilder.Entity<BankAccountHead>().OwnsOne(x => x.AuditFields);
      modelBuilder.Entity<BankAccountHead>().HasOne(x => x.AccountHead);
      modelBuilder.Entity<BankAccountHead>().HasOne(x => x.Bank);
      modelBuilder.Entity<BankBranch>().OwnsOne(x => x.AuditFields);
      modelBuilder.Entity<BankAccount>().OwnsOne(x => x.AuditFields);
      modelBuilder.Entity<BankAccount>().HasOne(x => x.BankBranch);
      modelBuilder.Entity<MidNightTaskTrackingInfo>().HasAlternateKey(x => x.Date);
      modelBuilder.Entity<StudentWaiver>().OwnsOne(x => x.AuditFields);
      modelBuilder.Entity<TermTest>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<TermTestMark>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<NotificationConfiguration>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<Attendance>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<ServiceCharge>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<BillingData>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<SslPaymentGateway>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<SalaryComponentType>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<PayrollComponent>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<SalaryComponent>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<OtherComponent>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<EmployeeSalary>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<SalarySheet>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<CommitteeMemberPersonalInfo>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<TaxRate>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<PendingBillInfo>().OwnsOne(p => p.AuditFields);
      modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
    }
  }
}