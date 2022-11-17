using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility;
using System.Linq;

namespace SinePulse.EMS.UseCases.Employee
{
  public class AddEmployeeUseCase : IAddEmployeeUseCase
  {
    private readonly IRepository _repository;
    private const int EmployeeCodeLength = 6;

    public AddEmployeeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public Domain.Employees.Employee AddEmployee(AddEmployeeRequestMessage request)
    {
      var designation = _repository.GetById<Designation>(request.DesignationId);
      var jobType = _repository.GetById<JobType>(request.JobTypeId);
      var reportTo = _repository.GetById<Domain.Employees.Employee>(request.ReportToId==null?0: request.ReportToId.Value);
      var grade = _repository.GetById<Grade>(request.GradeId);
      var employeeCode = GetEmployeeCode();

      Association association = GetAssociation(request.AssociationType, request.ObjectId);

      Domain.Employees.Employee employee = CreateEmployeeObject(request.EmployeeType);

      employee.BankAccountNo = request.BankAccountNo;
      employee.DOB = request.DOB;
      employee.EmailAddress = request.EmailAddress;
      employee.EmployeeType = request.EmployeeType;
      employee.FullName = request.FullName;
      employee.EmployeeCode = employeeCode;
      employee.JobType = jobType;
      employee.JoiningDate = request.JoiningDate;
      employee.MobileNo = request.MobileNo;
      employee.NationalId = request.NationalId;
      employee.Designation = designation;
      employee.Status = StatusEnum.Active;
      employee.Grade = grade;
      employee.AssociatedWith = association.AssociatedWith;
      employee.Institute = association.Institute;
      employee.Branch = association.Branch;
      employee.BranchMedium = association.BranchMedium;
      employee.ReportTo = reportTo;
      employee.AuditFields = new AuditFields
      {
        InsertedBy = request.CurrentUserName,
        InsertedDateTime = DateTime.Now,
        LastUpdatedBy = request.CurrentUserName,
        LastUpdatedDateTime = DateTime.Now
      };
      _repository.Insert(employee);
      return employee;
    }

    private Domain.Employees.Employee CreateEmployeeObject(EmployeeTypeEnum employeeType)
    {
      if (employeeType == EmployeeTypeEnum.Teacher) return new Teacher();

      return new Staff();
    }
    private Association GetAssociation(AssociationType associationType, long? objectId)
    {
      Association association = new Association();

      switch (associationType)
      {
        case AssociationType.Institute:
          var institute = _repository.GetById<Institute>((long)objectId);
          association.AssociatedWith = associationType;
          association.Institute = institute;
          association.Branch = null;
          association.BranchMedium = null;
          return association;
        case AssociationType.Branch:
          var branch = _repository.GetById<Branch, Institute>((long)objectId, b=>b.Institute);
          association.AssociatedWith = associationType;
          association.Institute = branch.Institute;
          association.Branch = branch;
          association.BranchMedium = null;
          return association;
        case AssociationType.BranchMedium:
          var branchMedium = _repository.GetById<BranchMedium, Branch, Institute>((long)objectId, e => e.Branch, e => e.Branch.Institute);
          association.AssociatedWith = associationType;
          association.Institute = branchMedium.Branch.Institute;
          association.Branch = branchMedium.Branch;
          association.BranchMedium = branchMedium;
          return association;
        default:
          association.AssociatedWith = associationType;
          association.Institute = null;
          association.Branch = null;
          association.BranchMedium = null;
          return association;
      }
    }
    private string GetEmployeeCode()
    {
      string key = StringGenerator.GenerateUniqueNumberKey(EmployeeCodeLength);

      while (IsEmployeeCodeExist(key))
      {
        key = StringGenerator.GenerateUniqueNumberKey(EmployeeCodeLength);
      }

      return key;
    }
    private bool IsEmployeeCodeExist(string epployeeCode)
    {
      return _repository.Filter<Domain.Employees.Employee>(e=>e.EmployeeCode == epployeeCode).Any();
    }
  }
}