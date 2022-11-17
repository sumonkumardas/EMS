using System;
using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.OtherComponentMessages;
using SinePulse.EMS.Messages.SalaryComponentMessages;

namespace SinePulse.EMS.Messages.PayrollMessages
{
  public class DefineSalaryRequestMessage : IRequest<DefineSalaryResponseMessage>
  {
    public long EmployeeId { get; set; }
    public decimal[] SalaryComponentAmounts { get; set; }
    public DateTime EffectiveDate { get; set; }
    public List<ShowSalaryComponentListResponseMessage.SalaryComponent> SalaryComponents { get; set; }
    public List<GetOtherComponentsListResponseMessage.OtherComponent> OtherComponents { get; set; }
    public string CurrentUserName { get; set; }
    public long? EmployeeGradeId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TaxDeduction { get; set; }
    public decimal[] OtherComponentAmounts { get; set; }
  }
}