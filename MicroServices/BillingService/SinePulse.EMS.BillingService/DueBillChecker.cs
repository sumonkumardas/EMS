
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.BillingService.Model;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Billing;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.BillingService
{
  public class DueBillChecker : IDueBillChecker
  {
    private readonly IRepository _repository;
    private readonly ICurrentSessionProvider _currentSessionProvider;
    private readonly EmsDbContext _dbContext;

    public DueBillChecker(IRepository repository, ICurrentSessionProvider currentSessionProvider, EmsDbContext dbContext)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
      _dbContext = dbContext;
    }
    public List<DueBillOfBranchMedium> GetDueBillsOfBranchMedium()
    {
      var dueBillsOfBranchMedium = new List<DueBillOfBranchMedium>();
      try
      {

        var serviceCharges = GetServiceCharges();

        var billingDate = DateTime.Now.AddMonths(-1);

        if (serviceCharges != null && serviceCharges.Any())
          dueBillsOfBranchMedium = GenerateDueBill(serviceCharges, billingDate, dueBillsOfBranchMedium);

        _dbContext.SaveChanges();
      }
      catch (Exception ex)
      {
        // ignored
      }

      return dueBillsOfBranchMedium;

    }

    private List<DueBillOfBranchMedium> GenerateDueBill(IQueryable<ServiceCharge> serviceCharges, DateTime billingDate, List<DueBillOfBranchMedium> dueBillsOfBranchMedium)
    {
      foreach (var serviceCharge in serviceCharges)
      {
        var currentSession = _currentSessionProvider.GetCurrentSession(serviceCharge.BranchMedium.Id);

        var totalStudents = GetTotalStudents(serviceCharge, currentSession);

        if (totalStudents == 0) continue;
        {
          var dueBill = totalStudents * serviceCharge.PerStudentCharge;

          SavePendingBillInfo(serviceCharge, dueBill, billingDate, totalStudents);

          dueBillsOfBranchMedium = GetDueBillDataList(serviceCharge, dueBillsOfBranchMedium, dueBill);
        }
      }

      return dueBillsOfBranchMedium;
    }

    private int GetTotalStudents(ServiceCharge serviceCharge, Session currentSession)
    {
      var totalStudents = _repository.Table<Student>(new[] { nameof(BranchMedium), nameof(Session) }).Count(x =>
          x.BranchMedium.Id == serviceCharge.BranchMedium.Id && x.Session.Id == currentSession.Id);
      return totalStudents;
    }

    private List<DueBillOfBranchMedium> GetDueBillDataList(ServiceCharge serviceCharge, List<DueBillOfBranchMedium> dueBillsOfBranchMedium, decimal dueBill)
    {
      var instituteEmployees = GetInstituteEmployees(serviceCharge);

      dueBillsOfBranchMedium =
        GetInstituteAdminBillingData(instituteEmployees, ref dueBillsOfBranchMedium, serviceCharge, dueBill);

      var branchEmployees = GetBranchEmployees(serviceCharge);

      dueBillsOfBranchMedium =
        GetBranchAdminBillingData(branchEmployees, ref dueBillsOfBranchMedium, serviceCharge, dueBill);

      var branchMediumEmployees = GetBranchMediumEmployees(serviceCharge);

      dueBillsOfBranchMedium =
        GetBranchMediumAdminBillingData(branchMediumEmployees, ref dueBillsOfBranchMedium, serviceCharge, dueBill);
      return dueBillsOfBranchMedium;
    }

    private List<DueBillOfBranchMedium> GetBranchMediumAdminBillingData(IQueryable<Employee> branchMediumEmployees, ref List<DueBillOfBranchMedium> dueBillsOfBranchMedium,
      ServiceCharge serviceCharge, decimal dueBill)
    {
      var branchMediumRoleId = _dbContext.Roles.FirstOrDefault(x => x.RoleType == RoleTypeEnum.BranchMediumAdmin)?.Id;
      foreach (var branchMediumEmployee in branchMediumEmployees)
      {
        {
          var user = _dbContext.UserRoles.FirstOrDefault(x =>
            x.RoleId == branchMediumRoleId && x.UserId == branchMediumEmployee.LoginUser.Id);

          if (user != null)
          {
            AddDueBillOfBranchMedium(ref dueBillsOfBranchMedium, serviceCharge, dueBill, branchMediumEmployee);
          }
        }
      }

      return dueBillsOfBranchMedium;
    }

    private List<DueBillOfBranchMedium> GetBranchAdminBillingData(IQueryable<Employee> branchEmployees, ref List<DueBillOfBranchMedium> dueBillsOfBranchMedium,
      ServiceCharge serviceCharge, decimal dueBill)
    {
      var branchRoleId = _dbContext.Roles.FirstOrDefault(x => x.RoleType == RoleTypeEnum.BranchAdmin)?.Id;
      foreach (var branchEmployee in branchEmployees)
      {
        {
          var user = _dbContext.UserRoles.FirstOrDefault(x =>
            x.RoleId == branchRoleId && x.UserId == branchEmployee.LoginUser.Id);

          if (user != null)
          {
            AddDueBillOfBranchMedium(ref dueBillsOfBranchMedium, serviceCharge, dueBill, branchEmployee);
          }
        }
      }

      return dueBillsOfBranchMedium;
    }

    private List<DueBillOfBranchMedium> GetInstituteAdminBillingData(IQueryable<Employee> instituteEmployees, ref List<DueBillOfBranchMedium> dueBillsOfBranchMedium,
      ServiceCharge serviceCharge, decimal dueBill)
    {
      var instituteRoleId = _dbContext.Roles.FirstOrDefault(x => x.RoleType == RoleTypeEnum.InstituteAdmin)?.Id;
      foreach (var instituteEmployee in instituteEmployees)
      {
        {
          var user = _dbContext.UserRoles.FirstOrDefault(x =>
            x.RoleId == instituteRoleId && x.UserId == instituteEmployee.LoginUser.Id);

          if (user != null)
          {
            AddDueBillOfBranchMedium(ref dueBillsOfBranchMedium, serviceCharge, dueBill, instituteEmployee);
          }
        }
      }

      return dueBillsOfBranchMedium;
    }

    private IQueryable<Employee> GetBranchMediumEmployees(ServiceCharge serviceCharge)
    {
      var branchMediumEmployees = _repository.Table<Employee>(new[] { nameof(Employee.LoginUser) }).Where(x =>
          x.AssociatedWith == AssociationType.BranchMedium &&
          x.Institute.Id == serviceCharge.BranchMedium.Branch.Institute.Id
          && x.LoginUser != null);
      return branchMediumEmployees;
    }

    private IQueryable<Employee> GetBranchEmployees(ServiceCharge serviceCharge)
    {
      var branchEmployees = _repository.Table<Employee>(new[] { nameof(Employee.LoginUser) }).Where(x =>
          x.AssociatedWith == AssociationType.Branch &&
          x.Institute.Id == serviceCharge.BranchMedium.Branch.Institute.Id
          && x.LoginUser != null);
      return branchEmployees;
    }

    private IQueryable<Employee> GetInstituteEmployees(ServiceCharge serviceCharge)
    {
      var instituteEmployees = _repository.Table<Employee>(new[] { nameof(Employee.LoginUser) }).Where(x =>
          x.AssociatedWith == AssociationType.Institute &&
          x.Institute.Id == serviceCharge.BranchMedium.Branch.Institute.Id
          && x.LoginUser != null);
      return instituteEmployees;
    }

    private static void AddDueBillOfBranchMedium(ref List<DueBillOfBranchMedium> dueBillsOfBranchMedium, ServiceCharge serviceCharge, decimal dueBill,
      Employee instituteEmployee)
    {
      dueBillsOfBranchMedium.Add(new DueBillOfBranchMedium()
      {
        BranchMediumId = serviceCharge.BranchMedium.Id,
        Month = DateTime.Now.ToString("MMMM"),
        BranchMediumName = serviceCharge.BranchMedium.Title(),
        DueBill = dueBill,
        Email = instituteEmployee.EmailAddress,
        MobileNo = instituteEmployee.MobileNo,
        Year = DateTime.Now.Year.ToString()
      });
    }

    private IQueryable<ServiceCharge> GetServiceCharges()
    {
      var serviceCharges = _repository.Table<ServiceCharge>(new[]
      {
        nameof(BranchMedium),
        nameof(BranchMedium) + "." + nameof(BranchMedium.Branch),
        nameof(BranchMedium) + "." + nameof(BranchMedium.Medium),
        nameof(BranchMedium) + "." + nameof(BranchMedium.Branch) + "." + nameof(BranchMedium.Branch.Institute)
      });
      return serviceCharges;
    }

    private void SavePendingBillInfo(ServiceCharge serviceCharge, decimal dueBill, DateTime billingDate, int totalStudents)
    {
      var pendingBillInfo = new PendingBillInfo()
      {
        BranchMedium = serviceCharge.BranchMedium,
        Amount = dueBill,
        Month = (MonthsOfYearEnum)billingDate.Month,
        PaymentDuetDate = DateTime.Now.AddDays(serviceCharge.PaymentBufferPeriod),
        PerStudentCharge = serviceCharge.PerStudentCharge,
        TotalStudents = totalStudents,
        Year = billingDate.Year,
        AuditFields = new AuditFields()
        {
          InsertedBy = "billing service",
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = "billing service",
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _repository.Insert(pendingBillInfo);
    }
  }
}
