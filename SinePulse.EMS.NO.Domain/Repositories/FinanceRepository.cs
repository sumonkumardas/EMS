using NakedObjects;
using NakedObjects.Menu;
using NakedObjects.Services;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Repositories
{
  [DisplayName("Finance")]
  public class FinanceRepository : AbstractFactoryAndRepository
  {
    public static void Menu(IMenu menu)
    {
      IMenu accounts = menu.CreateSubMenu("Accounts");
      accounts.AddAction("ShowAllAccountTypes");
      accounts.AddAction("CreateAccountHead");
      accounts.AddAction("ShowAllAccountHeads");

      IMenu feeSetup = menu.CreateSubMenu("Academic Fee Setup");
      feeSetup.AddAction("SetupMonthlyAcademicFee");
      feeSetup.AddAction("SetupYearlyAcademicFee");
    }

    #region Account Type
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "AccountTypeName", "FinancialStatement", "CodingStartValue", "CodingEndValue")]
    public IList<NOAccountType> ShowAllAccountTypes()
    {
      return Container.Instances<NOAccountType>().ToList();
    }
    #endregion

    #region Account Head
    public NOAccountHead CreateAccountHead(string accountCode, string accountHeadName, NOAccountType accountType, NOAccountHead parentHead, 
      AccountHeadTypeEnum accountHeadType, AccountPeriodEnum accountPeriod, bool isLedger)
    {
      NOAccountHead accountHead = Container.NewTransientInstance<NOAccountHead>();
      accountHead.AccountCode = accountCode;
      accountHead.AccountHeadName = accountHeadName;
      accountHead.ParentHead = parentHead;
      accountHead.AccountHeadType = accountHeadType;
      accountHead.AccountType = accountType;
      accountHead.AccountPeriod = accountPeriod;
      accountHead.IsLedger = isLedger;
      accountHead.Status = StatusEnum.Active;
      Container.Persist(ref accountHead);
      return accountHead;
    }
    public IList<NOAccountHead> Choices3CreateAccountHead(NOAccountType accountType)
    {
      if (accountType != null) return Container.Instances<NOAccountHead>().Where(w => w.AccountType.Id == accountType.Id && !w.IsLedger).ToList();

      return new List<NOAccountHead>();
    }
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "AccountCode", "AccountHeadName", "AccountType", "ParentHead", "AccountHeadType", "AccountPeriod")]
    public IList<NOAccountHead> ShowAllAccountHeads()
    {
      return Container.Instances<NOAccountHead>().ToList();
    }
    #endregion

    #region Academic Fee Setup
    public NOAcademicFee SetupMonthlyAcademicFee(NOInstitute institute, NOBranch branch, BranchMedium medium, 
      NOSession session, NOClass noClass, NOAccountHead accountHead, decimal fees)
    {
      NOAcademicFee academicFee = Container.NewTransientInstance<NOAcademicFee>();
      academicFee.BranchMedium = medium;
      academicFee.Session = session;
      academicFee.Class = noClass;
      academicFee.AccountHead = accountHead;
      academicFee.Fees = fees;
      Container.Persist(ref academicFee);
      return academicFee;
    }
    public IList<NOInstitute> Choices0SetupMonthlyAcademicFee()
    {
      return Container.Instances<NOInstitute>().ToList();
    }
    public IList<NOBranch> Choices1SetupMonthlyAcademicFee(NOInstitute institute)
    {
      if (institute != null) return institute.Branches;

      return new List<NOBranch>();
    }
    public IList<BranchMedium> Choices2SetupMonthlyAcademicFee(NOBranch branch)
    {
      if (branch != null) return branch.Mediums;

      return new List<BranchMedium>();
    }
    public IList<NOSession> Choices3SetupMonthlyAcademicFee(BranchMedium medium)
    {
      if (medium != null) return medium.Sessions;

      return new List<NOSession>();
    }
    public IList<NOClass> Choices4SetupMonthlyAcademicFee(BranchMedium medium, NOSession session)
    {
      if (medium != null && session != null) return medium.Classes.Where(w => w.Session.Id == session.Id).Select(s => s.Class).ToList();

      return new List<NOClass>();
    }
    public IList<NOAccountHead> Choices5SetupMonthlyAcademicFee()
    {
      return Container.Instances<NOAccountHead>().Where(w => w.AccountHeadType == AccountHeadTypeEnum.Academic && w.AccountPeriod == AccountPeriodEnum.Monthly).ToList();
    }

    public NOAcademicFee SetupYearlyAcademicFee(NOInstitute institute, NOBranch branch, BranchMedium medium, 
      NOSession session, NOClass noClass, NOAccountHead accountHead, decimal fees)
    {
      NOAcademicFee academicFee = Container.NewTransientInstance<NOAcademicFee>();
      academicFee.BranchMedium = medium;
      academicFee.Session = session;
      academicFee.Class = noClass;
      academicFee.AccountHead = accountHead;
      academicFee.Fees = fees;
      Container.Persist(ref academicFee);
      return academicFee;
    }
    public IList<NOInstitute> Choices0SetupYearlyAcademicFee()
    {
      return Container.Instances<NOInstitute>().ToList();
    }
    public IList<NOBranch> Choices1SetupYearlyAcademicFee(NOInstitute institute)
    {
      if (institute != null) return institute.Branches;

      return new List<NOBranch>();
    }
    public IList<BranchMedium> Choices2SetupYearlyAcademicFee(NOBranch branch)
    {
      if (branch != null) return branch.Mediums;

      return new List<BranchMedium>();
    }
    public IList<NOSession> Choices3SetupYearlyAcademicFee(BranchMedium medium)
    {
      if (medium != null) return medium.Sessions;

      return new List<NOSession>();
    }
    public IList<NOClass> Choices4SetupYearlyAcademicFee(BranchMedium medium, NOSession session)
    {
      if (medium != null && session != null) return medium.Classes.Where(w=>w.Session.Id == session.Id).Select(s => s.Class).ToList();

      return new List<NOClass>();
    }
    public IList<NOAccountHead> Choices5SetupYearlyAcademicFee()
    {
      return Container.Instances<NOAccountHead>().Where(w => w.AccountHeadType == AccountHeadTypeEnum.Academic && w.AccountPeriod == AccountPeriodEnum.Yearly).ToList();
    }
    #endregion
  }
}
