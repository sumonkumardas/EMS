using System;
using System.Collections;
using System.Collections.Generic;
using SinePulse.EMS.Messages.OtherComponentMessages;
using SinePulse.EMS.Messages.SalaryComponentMessages;

namespace SinePulse.EMS.Site.Models
{
  public class DefineSalaryViewModel : BaseViewModel
  {
    public long EmployeeId { get; set; }
    public string EmployeeNameAndCode { get; set; }
    public string EmployeeGrade { get; set; }
    public string EmployeeDesignation { get; set; }
    public long? EmployeeGradeId { get; set; }
    public string GradeMinSalary { get; set; }
    public string GradeMaxSalary { get; set; }
    public decimal[] SalaryComponentAmounts { get; set; }
    public DateTime EffectiveDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TaxDeduction { get; set; }
    public List<ShowSalaryComponentListResponseMessage.SalaryComponent> SalaryComponents { get; set; }
    public decimal[] OtherComponentAmounts { get; set; }
    public List<GetOtherComponentsListResponseMessage.OtherComponent> OtherComponents { get; set; }
  }
}