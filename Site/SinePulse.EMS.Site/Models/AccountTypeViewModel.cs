using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SinePulse.EMS.Domain.Enums;
using ChartOfAccountTypeEnum = SinePulse.EMS.Messages.Model.Enums.ChartOfAccountTypeEnum;
using FinancialStatementsEnum = SinePulse.EMS.Messages.Model.Enums.FinancialStatementsEnum;

namespace SinePulse.EMS.Site.Models
{
  public class AccountTypeViewModel : BaseViewModel
  {
    #region Primitive Properties
    public long Id { get; set; }
    public ChartOfAccountTypeEnum AccountTypeName { get; set; }
    public FinancialStatementsEnum FinancialStatement { get; set; }
    public virtual AccountTransactionTypeEnum TransactionType { get; set; }
    public int CodingStartValue { get; set; }
    public int CodingEndValue { get; set; }

    #endregion
  }
}
