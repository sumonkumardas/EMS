using System;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.EmployeePersonalInfoMessages
{
  public class GetEmployeePersonalInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public EmployeePersonalInfo EmployeePersonalInfos { get; }

    public GetEmployeePersonalInfoResponseMessage(ValidationResult validationResult,
      EmployeePersonalInfo employeePersonalInfos)
    {
      ValidationResult = validationResult;
      EmployeePersonalInfos = employeePersonalInfos;
    }

    public class EmployeePersonalInfo
    {
      public DateTime? DOB { get; set; }
      public bool IsAccountRegistered { get; set; }
      public string NationalId { get; set; }
      public string MobileNo { get; set; }
      public string EmailAddress { get; set; }
      public DateTime? JoiningDate { get; set; }
      public string BankAccountNo { get; set; }
      public string EmployeeType { get; set; }
      public string ReportTo { get; set; }
      public long ReportToEmployeeId { get; set; }
      public string FathersName { get; set; }
      public string MothersName { get; set; }
      public string SpouseName { get; set; }
      public string Gender { get; set; }
      public GenderEnum GenderEnum { get; set; }
      public string Religion { get; set; }
      public ReligionEnum ReligionEnum { get; set; }
      public string BloodGroup { get; set; }
      public BloodGroupEnum BloodGroupEnum { get; set; }
    }
  }
}