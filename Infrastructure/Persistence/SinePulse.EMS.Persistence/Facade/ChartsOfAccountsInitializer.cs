using System;
using System.Linq;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Persistence.Facade
{
  public class ChartsOfAccountsInitializer : IChartsOfAccountsInitializer
  {
    private readonly EmsDbContext _dbContext;

    public ChartsOfAccountsInitializer(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void InitializeChartsOfAccounts()
    {
      if (IsAlreadyCreatedChartsOfAccounts())
      {
        return;
      }

      CreateChartsOfAccounts();
      _dbContext.SaveChanges();
    }

    private bool IsAlreadyCreatedChartsOfAccounts()
    {
      return _dbContext.Set<AccountType>().Any();
    }

    private void CreateChartsOfAccounts()
    {
      CreateAssetTypeAccounts();
      CreateLiabilitiesTypeAccounts();
      CreateEquityTypeAccounts();
      CreateRevenueTypeAccounts();
      CreateExpenseTypeAccounts();

      InsertAutoPostingConfiguration(VoucherTypeEnum.BankVoucher, "1110");
      InsertAutoPostingConfiguration(VoucherTypeEnum.CashVoucher, "1120");
      InsertAutoPostingConfiguration(VoucherTypeEnum.YearClosing, "3220");
      InsertAutoPostingConfiguration(VoucherTypeEnum.WagesExpense, "5120");
      InsertAutoPostingConfiguration(VoucherTypeEnum.PayrollTaxExpense, "5130");
      InsertAutoPostingConfiguration(VoucherTypeEnum.WagesPayable, "2130");
      InsertAutoPostingConfiguration(VoucherTypeEnum.PayrollTaxPayable, "2140");
    }

    private void InsertAutoPostingConfiguration(VoucherTypeEnum voucherType, string accountCode)
    {
      var autoPostingConfig = new AutoPostingConfiguration
      {
        VoucherType = voucherType,
        AccountCode = accountCode,
        AuditFields =
        {
          InsertedBy = "Automated",
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = "Automated",
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _dbContext.AutoPostingConfigurations.Add(autoPostingConfig);
    }

    private void CreateExpenseTypeAccounts()
    {
      var expenseAccountType = CreateAccountType(ChartOfAccountTypeEnum.Expense, FinancialStatementsEnum.IncomeStatement, AccountTransactionTypeEnum.Debit, 5000, 5999);

      var expenseAccountHead = CreateAccountHead("5000", "Expense", expenseAccountType, null, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);

      var fixedExpenseAccountHead = CreateAccountHead("5100", "Fixed", expenseAccountType, expenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("5110", "Rent", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("5111", "House Rent", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true);

      CreateAccountHead("5120", "Wages/Salaries Expense", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.Payroll, AccountPeriodEnum.Monthly, true);
      CreateAccountHead("5130", "Payroll Tax Expense", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.Payroll, AccountPeriodEnum.Monthly, true);
      CreateAccountHead("5140", "Company Car Expenses", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("5150", "Website Hosting", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);

      var utilityExpenseAccountHead = CreateAccountHead("5150", "Utilities", expenseAccountType, fixedExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("5151", "Telephone Bills", expenseAccountType, utilityExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true);
      CreateAccountHead("5152", "Mobile Bills", expenseAccountType, utilityExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true);
      CreateAccountHead("5153", "Electricity Bills", expenseAccountType, utilityExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true);
      CreateAccountHead("5154", "Internet Bills", expenseAccountType, utilityExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true);

      var variableExpenseAccountHead = CreateAccountHead("5200", "Variable", expenseAccountType, expenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("5210", "Advertising", expenseAccountType, variableExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("5220", "Freight", expenseAccountType, variableExpenseAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);

      CreateAccountTransactionType(expenseAccountType, AccountTransactionTypeEnum.Debit, IncreaseDecreaseEnum.Increase);
      CreateAccountTransactionType(expenseAccountType, AccountTransactionTypeEnum.Credit, IncreaseDecreaseEnum.Decrease);
    }
    private void CreateRevenueTypeAccounts()
    {
      var incomeAccountType = CreateAccountType(ChartOfAccountTypeEnum.Revenue, FinancialStatementsEnum.IncomeStatement, AccountTransactionTypeEnum.Credit, 4000, 4999);

      var revenueAccountHead = CreateAccountHead("4000", "Revenue", incomeAccountType, null, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      var feesAccountHead = CreateAccountHead("4100", "Fees", incomeAccountType, revenueAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.None, false);

      var monthlyFeesAccountHead = CreateAccountHead("4110", "Monthly Fees", incomeAccountType, feesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Monthly, false);
      CreateAccountHead("4111", "Tuition Fees", incomeAccountType, monthlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Monthly, true);
      CreateAccountHead("4112", "Absent Fees", incomeAccountType, monthlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Monthly, true);
      CreateAccountHead("4113", "ICT Fees", incomeAccountType, monthlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Monthly, true);

      var yearlyFeesAccountHead = CreateAccountHead("4120", "Yearly Fees", incomeAccountType, feesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Yearly, false);
      CreateAccountHead("4121", "Admission Fees", incomeAccountType, yearlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Yearly, true);
      CreateAccountHead("4122", "Development Fees", incomeAccountType, yearlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Yearly, true);
      CreateAccountHead("4123", "ID Card Fees", incomeAccountType, yearlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Yearly, true);
      CreateAccountHead("4124", "Exam Fees", incomeAccountType, yearlyFeesAccountHead, AccountHeadTypeEnum.Academic, AccountPeriodEnum.Yearly, true);

      CreateAccountTransactionType(incomeAccountType, AccountTransactionTypeEnum.Debit, IncreaseDecreaseEnum.Decrease);
      CreateAccountTransactionType(incomeAccountType, AccountTransactionTypeEnum.Credit, IncreaseDecreaseEnum.Increase);
    }
    private void CreateAssetTypeAccounts()
    {
      var assetAccountType = CreateAccountType(ChartOfAccountTypeEnum.Assets, FinancialStatementsEnum.BalanceSheet, AccountTransactionTypeEnum.Debit, 1000, 1999);

      var assetAccountHead = CreateAccountHead("1000", "Assets", assetAccountType, null, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);

      var currentAssetAccountHead = CreateAccountHead("1100", "Current Assets", assetAccountType, assetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      var bankAccountHead = CreateAccountHead("1110", "Bank", assetAccountType, currentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);

      CreateAccountHead("1120", "Cash on Hand", assetAccountType, currentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true);
      CreateAccountHead("1130", "Petty Cash", assetAccountType, currentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, true);
      CreateAccountHead("1140", "Debtors/Accounts Receivable", assetAccountType, currentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("1150", "Stock on Hand", assetAccountType, currentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);

      var noneCurrentAssetAccountHead = CreateAccountHead("1200", "Non-Current Assets", assetAccountType, assetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("1210", "Furniture and Fittings", assetAccountType, noneCurrentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("1220", "Office Equipments", assetAccountType, noneCurrentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("1230", "Company Car", assetAccountType, noneCurrentAssetAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);

      CreateAccountTransactionType(assetAccountType, AccountTransactionTypeEnum.Debit, IncreaseDecreaseEnum.Increase);
      CreateAccountTransactionType(assetAccountType, AccountTransactionTypeEnum.Credit, IncreaseDecreaseEnum.Decrease);
    }
    private void CreateLiabilitiesTypeAccounts()
    {
      var liabilityAccountType = CreateAccountType(ChartOfAccountTypeEnum.Liabilities, FinancialStatementsEnum.BalanceSheet, AccountTransactionTypeEnum.Credit, 2000, 2999);

      var liabilityAccountHead = CreateAccountHead("2000", "Liabilities", liabilityAccountType, null, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);

      var currentLiabilityAccountHead = CreateAccountHead("2100", "Current Liabilities", liabilityAccountType, liabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("2110", "Bank Overdraft", liabilityAccountType, currentLiabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("2120", "Creditors/Accounts Payable", liabilityAccountType, currentLiabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("2130", "Wages Payable", liabilityAccountType, currentLiabilityAccountHead, AccountHeadTypeEnum.Payroll, AccountPeriodEnum.Monthly, true);
      CreateAccountHead("2140", "Payroll Taxes Payable", liabilityAccountType, currentLiabilityAccountHead, AccountHeadTypeEnum.Payroll, AccountPeriodEnum.Monthly, true);

      var nonCurrentLiabilityAccountHead = CreateAccountHead("2200", "Non-Current Liabilities", liabilityAccountType, liabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("2210", "Company Car Loan", liabilityAccountType, nonCurrentLiabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("2220", "Equipment Loan", liabilityAccountType, nonCurrentLiabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("2230", "Long Term Loan", liabilityAccountType, nonCurrentLiabilityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);

      CreateAccountTransactionType(liabilityAccountType, AccountTransactionTypeEnum.Debit, IncreaseDecreaseEnum.Decrease);
      CreateAccountTransactionType(liabilityAccountType, AccountTransactionTypeEnum.Credit, IncreaseDecreaseEnum.Increase);
    }
    private void CreateEquityTypeAccounts()
    {
      var equityAccountType = CreateAccountType(ChartOfAccountTypeEnum.Equity, FinancialStatementsEnum.BalanceSheet, AccountTransactionTypeEnum.Credit, 3000, 3999);

      var equityAccountHead = CreateAccountHead("3000", "Equity", equityAccountType, null, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("3110", "Owner's Capital", equityAccountType, equityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("3210", "Retained Earnings", equityAccountType, equityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);
      CreateAccountHead("3310", "Current Profit", equityAccountType, equityAccountHead, AccountHeadTypeEnum.General, AccountPeriodEnum.None, false);

      CreateAccountTransactionType(equityAccountType, AccountTransactionTypeEnum.Debit, IncreaseDecreaseEnum.Decrease);
      CreateAccountTransactionType(equityAccountType, AccountTransactionTypeEnum.Credit, IncreaseDecreaseEnum.Increase);
    }

    private AccountType CreateAccountType(ChartOfAccountTypeEnum accountTypeEnum, FinancialStatementsEnum financialStatementType,
      AccountTransactionTypeEnum transactionType, int codingStartValue, int codingEndValue)
    {
      var accountType = new AccountType
      {
        AccountTypeName = accountTypeEnum,
        FinancialStatement = financialStatementType,
        TransactionType = transactionType,
        CodingStartValue = codingStartValue,
        CodingEndValue = codingEndValue,
        AuditFields =
        {
          InsertedBy = "Automated",
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = "Automated",
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _dbContext.AccountTypes.Add(accountType);
      return accountType;
    }

    private AccountHead CreateAccountHead(string accountCode, string headName, AccountType accountType,
      AccountHead parentHead, AccountHeadTypeEnum headType, AccountPeriodEnum period, bool isLedger)
    {
      var accountHead = new AccountHead();
      accountHead.AccountCode = accountCode;
      accountHead.AccountHeadName = headName;
      accountHead.AccountType = accountType;
      accountHead.ParentHead = parentHead;
      accountHead.AccountHeadType = headType;
      accountHead.AccountPeriod = period;
      accountHead.IsLedger = isLedger;
      accountHead.Status = StatusEnum.Active;

      accountHead.AuditFields.InsertedBy = "Automated";
      accountHead.AuditFields.InsertedDateTime = DateTime.Now;
      accountHead.AuditFields.LastUpdatedBy = "Automated";
      accountHead.AuditFields.LastUpdatedDateTime = DateTime.Now;

      _dbContext.AccountHeads.Add(accountHead);

      return accountHead;
    }

    private void CreateAccountTransactionType(AccountType accountType, AccountTransactionTypeEnum debit,
      IncreaseDecreaseEnum increaseDecreaseType)
    {
      var accountTransactionType = new AccountTransactionType
      {
        AccountType = accountType,
        TransactionType = debit,
        IncreaseDecreaseType = increaseDecreaseType,
        AuditFields =
        {
          InsertedBy = "Automated",
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = "Automated",
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _dbContext.AccountTransactionTypes.Add(accountTransactionType);
    }
  }
}