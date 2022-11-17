using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Employee
{
  public class EditEmployeeUseCase : IEditEmployeeUseCase
  {
    private readonly IRepository _repository;

    public EditEmployeeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditEmployee(EditEmployeeRequestMessage request)
    {
      var designation = new Designation();
      designation = request.DesignationId == null ? null : _repository.GetById<Designation>(request.DesignationId.Value);
      var jobType = new JobType();
      jobType = request.JobTypeId == null ? null : _repository.GetById<JobType>(request.JobTypeId.Value);
      var reportTo = _repository.GetById<Domain.Employees.Employee>(request.ReportToId==null?0: request.ReportToId.Value);
      var grade = new Grade();
      grade = request.JobTypeId == null ? null : _repository.GetById<Grade>(request.GradeId.Value);
            var employee = _repository.GetById<Domain.Employees.Employee>(request.Id);

      Association association = GetAssociation(request.AssociationType, request.ObjectId);

      employee.BankAccountNo = request.BankAccountNo;
      employee.DOB = request.DOB;
      employee.EmailAddress = request.EmailAddress;
      employee.EmployeeType = request.EmployeeType;
      employee.FullName = request.FullName;
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

      employee.AuditFields.LastUpdatedBy = request.CurrentUserName;
      employee.AuditFields.LastUpdatedDateTime = DateTime.Now;
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
          var branch = _repository.GetById<Branch, Institute>((long)objectId, b => b.Institute);
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
  }
}