using NakedObjects;
using NakedObjects.Menu;
using NakedObjects.Services;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Designations;
using SinePulse.EMS.NO.Domain.Employees;
using SinePulse.EMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Repositories
{
  [DisplayName("Employees")]
  public class EmployeeRepository : AbstractFactoryAndRepository
  {
    private const int UserCodeLength = 6;
    public static void Menu(IMenu menu)
    {
      menu.AddAction("AddEmployee");
      menu.AddAction("ShowAllEmployees");
    }

    public NOEmployee AddEmployee(NOBranch branch, BranchMedium medium, string fullName, DateTime dOB, 
                                  string nationalId, string mobileNo, string emailAddress, DateTime joiningDate, 
                                  EmployeeTypeEnum employeeType,
                                  NODesignation designation, NOJobType jobType, NOGrade grade)
    {
      NOEmployee employee = Container.NewTransientInstance<NOEmployee>();
      employee.FullName = fullName;
      employee.UserCode = GetUserCode();
      employee.DOB = dOB;
      employee.NationalId = nationalId;
      employee.MobileNo = mobileNo;
      employee.EmailAddress = emailAddress;
      employee.JoiningDate = joiningDate;
      employee.EmployeeType = employeeType;
      employee.Designation = designation;
      employee.JobType = jobType;
      employee.Grade = grade;
      employee.Status = StatusEnum.Active;
      employee.Branch = medium;
      Container.Persist(ref employee);
      return employee;
    }
    public IList<NOBranch> Choices0AddEmployee()
    {
      return Container.Instances<NOBranch>().ToList();
    }
    public IList<BranchMedium> Choices1AddEmployee(NOBranch branch)
    {
      if (branch != null)
      {
        return Container.Instances<BranchMedium>().Where(w => w.Branch.Id == branch.Id).ToList();
      }
      else
      {
        return new List<BranchMedium>();
      }
    }
    public IList<NODesignation> Choices9AddEmployee(EmployeeTypeEnum employeeType)
    {
      return Container.Instances<NODesignation>().Where(w => w.EmployeeType == employeeType).ToList();
    }

    private string GetUserCode()
    {
      string key = StringGenerator.GenerateUniqueNumberKey(UserCodeLength);

      while (IsUserCodeExist(key))
      {
        key = StringGenerator.GenerateUniqueNumberKey(UserCodeLength);
      }

      return key;
    }

    private bool IsUserCodeExist(string code)
    {
      NOEmployee user = Container.Instances<NOEmployee>().Where(w => w.UserCode == code).FirstOrDefault();

      if (user != null)
      {
        return true;
      }

      return false;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "FullName", "MobileNo", "JoiningDate", "EmployeeType", "Designation", "Grade", "Branch")]
    public IList<NOEmployee> ShowAllEmployees()
    {
      return Container.Instances<NOEmployee>().ToList();
    }
  }
}
