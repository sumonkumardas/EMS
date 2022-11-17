using System;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Employees;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using EmployeeTypeEnum = SinePulse.EMS.Messages.Model.Enums.EmployeeTypeEnum;
using StatusEnum = SinePulse.EMS.Messages.Model.Enums.StatusEnum;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class ShowEmployeeListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<Employee> EmployeeList { get; }

    public ShowEmployeeListResponseMessage(ValidationResult validationResult, List<Employee> employeeList)
    {
      ValidationResult = validationResult;
      EmployeeList = employeeList;
    }
    
    public class Employee
    {
      public long Id { get; set; }
      public string FullName { get; set; }
      public string EmployeeCode { get; set; }
      public DateTime? DOB { get; set; }
      public string NationalId { get; set; }
      public string MobileNo { get; set; }
      public string EmailAddress { get; set; }
      public DateTime? JoiningDate { get; set; }
      public string BankAccountNo { get; set; }
      public EmployeeTypeEnum EmployeeType { get; set; }
      public StatusEnum Status { get; set; }
      public AssociationType AssociatedWith { get; set; }
      public long? ReportToId { get; set; }
      public string ImageUrl { get; set; }
    }
  }
}