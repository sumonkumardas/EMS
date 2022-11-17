using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Messages.EmployeeSalaryMessages;
using SinePulse.EMS.Messages.GenerateSalarySheetMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.OtherComponentMessages;
using SinePulse.EMS.Messages.PayrollMessages;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Messages.SalarySheetMessages;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Banks;
using SinePulse.EMS.UseCases.BranchMediumAccountsHeads;
using SinePulse.EMS.UseCases.Employee;
using SinePulse.EMS.UseCases.EmployeeSalaries;
using SinePulse.EMS.UseCases.GenerateSalarySheets;
using SinePulse.EMS.UseCases.OtherComponents;
using SinePulse.EMS.UseCases.Payrolls;
using SinePulse.EMS.UseCases.SalaryComponents;
using SinePulse.EMS.UseCases.SalarySheets;
using SinePulse.EMS.UseCases.Sessions;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace SinePulse.EMS.Site.Controllers
{
  public class PayrollController : BaseController
  {
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly ShowEmployeeRequestHandler _showEmployeeRequestHandler;
    private readonly ShowSalaryComponentListRequestHandler _showSalaryComponentListRequestHandler;
    private readonly DefineSalaryRequestHandler _defineSalaryRequestHandler;
    private readonly DefineSalaryResponsePresenter _defineSalaryResponsePresenter;
    private readonly GetEmployeeSalaryListRequestHandler _getEmployeeSalaryListRequestHandler;
    private readonly ICurrentSessionProvider _currentSessionProvider;
    private readonly GetBankAccountAccountHeadsRequestHandler _getBankAccountAccountHeadsRequestHandler;
    private readonly GetSalarySheetYearsRequestHandler _getSalarySheetYearsRequestHandler;
    private readonly SaveSalarySheetRequestHandler _saveSalarySheetRequestHandler;
    private readonly SaveSalarySheetResponsePresenter _saveSalarySheetResponsePresenter;
    private readonly GetOtherComponentsListRequestHandler _getOtherComponentsListRequestHandler;
    private readonly GetSalarySheetRequestHandler _getSalarySheetRequestHandler;
    private readonly ChangeButtonStatusRequestHandler _changeButtonStatusRequestHandler;
    private readonly SalarySheetAccountPostingRequestHandler _salarySheetAccountPostingRequestHandler;
    private readonly GetBankStatementRequestHandler _getBankStatementsRequestHandler;
    private readonly GetBankStatementInfoRequestHandler _getBankStatementInfoRequestHandler;

    public PayrollController(GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      ShowEmployeeRequestHandler showEmployeeRequestHandler,
      ShowSalaryComponentListRequestHandler showSalaryComponentListRequestHandler,
      DefineSalaryRequestHandler defineSalaryRequestHandler,
      DefineSalaryResponsePresenter defineSalaryResponsePresenter, 
      GetEmployeeSalaryListRequestHandler getEmployeeSalaryListRequestHandler, 
      ICurrentSessionProvider currentSessionProvider, 
      GetBankAccountAccountHeadsRequestHandler getBankAccountAccountHeadsRequestHandler, 
      GetSalarySheetYearsRequestHandler getSalarySheetYearsRequestHandler, 
      SaveSalarySheetRequestHandler saveSalarySheetRequestHandler, 
      SaveSalarySheetResponsePresenter saveSalarySheetResponsePresenter, 
      GetOtherComponentsListRequestHandler getOtherComponentsListRequestHandler, 
      GetSalarySheetRequestHandler getSalarySheetRequestHandler, 
      ChangeButtonStatusRequestHandler changeButtonStatusRequestHandler, 
      SalarySheetAccountPostingRequestHandler salarySheetAccountPostingRequestHandler, 
      IHostingEnvironment hostingEnvironment, 
      GetBankStatementRequestHandler getBankStatementsRequestHandler, 
      GetBankStatementInfoRequestHandler getBankStatementInfoRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showEmployeeRequestHandler = showEmployeeRequestHandler;
      _showSalaryComponentListRequestHandler = showSalaryComponentListRequestHandler;
      _defineSalaryRequestHandler = defineSalaryRequestHandler;
      _defineSalaryResponsePresenter = defineSalaryResponsePresenter;
      _getEmployeeSalaryListRequestHandler = getEmployeeSalaryListRequestHandler;
      _currentSessionProvider = currentSessionProvider;
      _getBankAccountAccountHeadsRequestHandler = getBankAccountAccountHeadsRequestHandler;
      _getSalarySheetYearsRequestHandler = getSalarySheetYearsRequestHandler;
      _saveSalarySheetRequestHandler = saveSalarySheetRequestHandler;
      _saveSalarySheetResponsePresenter = saveSalarySheetResponsePresenter;
      _getOtherComponentsListRequestHandler = getOtherComponentsListRequestHandler;
      _getSalarySheetRequestHandler = getSalarySheetRequestHandler;
      _changeButtonStatusRequestHandler = changeButtonStatusRequestHandler;
      _salarySheetAccountPostingRequestHandler = salarySheetAccountPostingRequestHandler;
      _hostingEnvironment = hostingEnvironment;
      _getBankStatementsRequestHandler = getBankStatementsRequestHandler;
      _getBankStatementInfoRequestHandler = getBankStatementInfoRequestHandler;
    }

    [HttpGet]
    public ActionResult DefineSalary(long employeeId)
    {
      var userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new DefineSalaryViewModel();
      var employeeInfo = GetEmployeeInfo(employeeId);
      var salaryComponents = GetSalaryComponentList();
      viewModel.EmployeeId = employeeId;
      viewModel.EmployeeNameAndCode = employeeInfo.FullName + " (" + employeeInfo.EmployeeCode + ")";
      viewModel.EmployeeDesignation = employeeInfo.Designation?.DesignationName;
      viewModel.EmployeeGradeId = employeeInfo.Grade?.Id;
      viewModel.EmployeeGrade = employeeInfo.Grade?.GradeTitle;
      viewModel.SalaryComponents = salaryComponents;
      viewModel.GradeMaxSalary = ""+employeeInfo.Grade?.MaxSalary;
      viewModel.GradeMinSalary = ""+employeeInfo.Grade?.MinSalary;
      viewModel.OtherComponents = GetOtherComponentList();

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      return View(viewModel);
    }

    [HttpPost]
    public ActionResult DefineSalary(DefineSalaryViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DefineSalaryRequestMessage();
      requestMessage.EmployeeId = model.EmployeeId;
      requestMessage.SalaryComponentAmounts = model.SalaryComponentAmounts;
      requestMessage.EffectiveDate = model.EffectiveDate;
      requestMessage.SalaryComponents = GetSalaryComponentList();
      requestMessage.OtherComponents = GetOtherComponentList();
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.EmployeeGradeId = model.EmployeeGradeId;
      requestMessage.TotalAmount = model.TotalAmount;
      requestMessage.TaxDeduction = model.TaxDeduction;
      requestMessage.OtherComponentAmounts = model.OtherComponentAmounts;
      var response = _defineSalaryRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _defineSalaryResponsePresenter.Handle(response.Result, new DefineSalaryViewModel(), ModelState,
        GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.EmployeeId = model.EmployeeId;
        viewModel.SalaryComponents = GetSalaryComponentList();
        viewModel.OtherComponents = GetOtherComponentList();
        viewModel.SalaryComponentAmounts = model.SalaryComponentAmounts;
        viewModel.OtherComponentAmounts = model.OtherComponentAmounts;
        viewModel.EmployeeGradeId = model.EmployeeGradeId;
        viewModel.TotalAmount = model.TotalAmount;
        viewModel.GradeMaxSalary = model.GradeMaxSalary;
        viewModel.GradeMinSalary = model.GradeMinSalary;
        return View(viewModel);
      }

      return RedirectToAction("ViewEmployee", "Employee", new {employeeId = model.EmployeeId});
    }

    [HttpGet]
    public ActionResult SaveSalarySheet(long branchMediumId, int? year, int? month)
    {
      var userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new SaveSalarySheetViewModel();
      viewModel.Years = GetSalarySheetYears(branchMediumId);
      viewModel.BankAccounts = GetBankAccounts(branchMediumId);
      viewModel.BranchMediumId = branchMediumId;
      viewModel.Year = year ?? DateTime.Now.Year;
      viewModel.Month = month == null ? (MonthType)DateTime.Now.Month : (MonthType)month.Value;
      
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      return View(viewModel);
    }

    [HttpPost]
    public ActionResult SaveSalarySheet(SaveSalarySheetViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new SaveSalarySheetRequestMessage();
      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.Month = (int) model.Month;
      requestMessage.Year = model.Year;
      requestMessage.EmployeeIds = model.EmployeeIds;
      requestMessage.TotalTaxDeduction = model.TotalTaxDeduction;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.EmployeeBankAccounts = model.EmployeeBankAccounts;
      if (model.TotalAmounts != null) requestMessage.TotalAmount = model.TotalAmounts.Sum();
      if (model.SalaryComponentAmounts != null) requestMessage.SalaryComponentAmounts = model.SalaryComponentAmounts;
      if (model.SalaryComponentNames != null) requestMessage.SalaryComponentNames = model.SalaryComponentNames;
      if (model.OtherDeductionAmounts != null) requestMessage.OtherDeductionAmounts = model.OtherDeductionAmounts;
      var response = _saveSalarySheetRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _saveSalarySheetResponsePresenter.Handle(response.Result, new SaveSalarySheetViewModel(), 
        ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        requestMessage.Month = (int) model.Month;
        requestMessage.Year = model.Year;
        viewModel.BranchMediumId = model.BranchMediumId;
        viewModel.Years = GetSalarySheetYears(model.BranchMediumId);
        viewModel.BankAccounts = GetBankAccounts(model.BranchMediumId);
        return View(viewModel);
      }
      viewModel.BankAccounts = GetBankAccounts(model.BranchMediumId);
      viewModel.BranchMediumId = model.BranchMediumId;
      viewModel.Years = GetSalarySheetYears(model.BranchMediumId);
      viewModel.Year = model.Year;
      viewModel.Month = model.Month;
      return View(viewModel);
    }

    public ActionResult PrintBankStatement(int year, int month, long branchMediumId,
      string bankAccountNumber)
    {
      var bankStatements = GetBankStatements(year, month, branchMediumId);
      var monthName = ((MonthsOfYearEnum) month).ToString("G");
      var fileName = "BankStatement-"+monthName+"-"+year+".xlsx";
      var xssfWorkbook = CreateBankStatementExcelSheet(year, branchMediumId, bankAccountNumber, monthName,
        bankStatements);
      var output =new MemoryStream();
      xssfWorkbook.Write(output);
      return File(output.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
    
    public ActionResult ExportSalarySheet(int year, int month, long branchMediumId)
    {
      var salarySheets = GetEmployeeSalarySheet(year, month, branchMediumId);
      var monthName = ((MonthsOfYearEnum) month).ToString("G");
      var fileName = "SalarySheet-"+monthName+"-"+year+".xlsx";
      var workbook = new XSSFWorkbook();
      var excelSheet = workbook.CreateSheet("Bank Statement");
      var row = excelSheet.CreateRow(0);
      row.CreateCell(0, CellType.String).SetCellValue("Sl. No.");
      row.CreateCell(1).SetCellValue("Employee Name");
      row.CreateCell(2).SetCellValue("Employee Code");
      row.CreateCell(3).SetCellValue("Designation");
      row.CreateCell(4).SetCellValue("Bank A/C");
      int columnIndex = 5;
      foreach (var component in salarySheets[0].SalaryComponents)
      {
        row.CreateCell(columnIndex++, CellType.String).SetCellValue(component.ComponentName);
      }
      row.CreateCell(columnIndex).SetCellValue("Total Amount");

      int serialNo = 1;
      foreach (var salarySheet in salarySheets)
      {
        row = excelSheet.CreateRow(serialNo);
        row.CreateCell(0, CellType.String).SetCellValue(serialNo++);
        row.CreateCell(1).SetCellValue(salarySheet.EmployeeName);
        row.CreateCell(2).SetCellValue(salarySheet.EmployeeCode);
        row.CreateCell(3).SetCellValue(salarySheet.Designation);
        row.CreateCell(4).SetCellValue(salarySheet.BankAccountNo);
        columnIndex = 5;
        foreach (var component in salarySheet.SalaryComponents)
        {
          row.CreateCell(columnIndex++, CellType.String).SetCellValue(""+component.Amount);
        }
        row.CreateCell(columnIndex).SetCellValue(GetTotalAmount(salarySheet.SalaryComponents));
      }
      var output =new MemoryStream();
      workbook.Write(output);
      return File(output.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    private string GetTotalAmount(List<GetSalarySheetResponseMessage.SalaryComponent> salarySheetSalaryComponents)
    {
      var sumOfComponents = salarySheetSalaryComponents
        .Where(s => s.ComponentType == IncreaseDecreaseEnum.Increase)
        .Select(s => s.Amount)
        .Sum();
      var deductions =  salarySheetSalaryComponents
        .Where(s => s.ComponentType == IncreaseDecreaseEnum.Decrease)
        .Select(s => s.Amount)
        .Sum();
      return (sumOfComponents - deductions).ToString();
    }

    private XSSFWorkbook CreateBankStatementExcelSheet(int year, long branchMediumId, string bankAccountNumber, 
      string monthName, List<GetBankStatementResponseMessage.BankStatement> bankStatements)
    {
        var bankStatementInfo = GetBankStatementInfos(branchMediumId, bankAccountNumber);
        var workbook = new XSSFWorkbook();
        var excelSheet = workbook.CreateSheet("Bank Statement");
        int index = 0;
        var row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue("Date: " + DateTime.Now.ToString("dd.MM.yyyy"));
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue("To");
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue("The Manager");
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue(bankStatementInfo.BankName);
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue(bankStatementInfo.BankBranchName + " Baranch");
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue(bankStatementInfo.BankBranchAddress);
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String)
          .SetCellValue("Subject: \t Salary Transfer to " + bankStatementInfo.InstituteName + " employees.");
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue("");
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue("Dear Sir,");
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue("Please transfer the monthly salary for " + monthName + 
                                                        ", " + year + " from " + bankStatementInfo.InstituteName);
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue("SND Account "+bankAccountNumber+
                                                        " to the following employees account: ");
        row = excelSheet.CreateRow(index++);
        row.CreateCell(0, CellType.String).SetCellValue("");

        row = excelSheet.CreateRow(index);
        row.CreateCell(0, CellType.String).SetCellValue("Sl. No.");
        row.CreateCell(1).SetCellValue("Employee Name");
        row.CreateCell(2).SetCellValue("Account Number");
        row.CreateCell(3).SetCellValue("Amount");

        foreach (var bankStatement in bankStatements)
        {
          row = excelSheet.CreateRow(bankStatement.SerialNo + index);
          row.CreateCell(0, CellType.String).SetCellValue(bankStatement.SerialNo);
          row.CreateCell(1).SetCellValue(bankStatement.EmployeeName);
          row.CreateCell(2).SetCellValue(bankStatement.AccountNumber);
          row.CreateCell(3).SetCellValue("" + bankStatement.Amount);
        }

        return workbook;
    }

    private List<GetBankStatementResponseMessage.BankStatement> GetBankStatements(int year, int month, long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetBankStatementRequestMessage();
      requestMessage.Year = year;
      requestMessage.Month = month;
      requestMessage.BranchMediumId = branchMediumId; 
      var response = _getBankStatementsRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.BankStatements;
    }


    [HttpPost]
    public JsonResult GetSalarySheet(int year, int month, long branchMediumId)
    {
      if (year > 0 && month > 0 && branchMediumId > 0)
      {
        var employeeSalarySheet = GetEmployeeSalarySheet(year, month, branchMediumId);
        if (employeeSalarySheet != null && employeeSalarySheet.Any())
        {
          return Json(employeeSalarySheet);
        }

        var employeeSalaries = GetEmployeeSalaries(branchMediumId);
        if (employeeSalaries != null && employeeSalaries.Any())
        {
          return Json(employeeSalaries);
        } 
        return Json(new {DataNotFound = "Data not Found!"});
      }

      return Json(new {InvalidParameters = "Invalid Parameters!"});
    }

    [HttpPost]
    public JsonResult ChangeButtonStatus(int year, int month, long branchMediumId)
    {
      if (year > 0 && month > 0 && branchMediumId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new ChangeButtonStatusRequestMessage();
        requestMessage.BranchMediumId = branchMediumId;
        requestMessage.Month = month;
        requestMessage.Year = year;     
        var response = _changeButtonStatusRequestHandler.Handle(requestMessage, cancellationToken);
        if (response.Result.Status != null)
          return Json(response.Result.Status);
        return Json(null);
      }
      return Json(null);
    }

    [HttpPost]
    public JsonResult PostSalarySheetAccount(int year, int month, long branchMediumId)
    {
      if (year == 0)
        return Json(new {selectYear = true});
      if (month == 0)
        return Json(new {selectMonth = true});
      var cancellationToken = new CancellationToken();
      var requestMessage = new SalarySheetAccountPostingRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      requestMessage.Month = month;
      requestMessage.Year = year;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      var response = _salarySheetAccountPostingRequestHandler.Handle(requestMessage, cancellationToken);
      return Json(response.Result.AccountPostResponseData);
    }

    private List<GetSalarySheetResponseMessage.EmployeeSalary> GetEmployeeSalarySheet(int year, int month, 
      long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetSalarySheetRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      requestMessage.Month = month;
      requestMessage.Year = year;     
      var response = _getSalarySheetRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.EmployeeSalaries;
    }

    private List<GetEmployeeSalaryListResponseMessage.EmployeeSalary> GetEmployeeSalaries(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetEmployeeSalaryListRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      var response = _getEmployeeSalaryListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.EmployeeSalaries;
    }

    private List<int> GetSalarySheetYears(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetSalarySheetYearsRequestMessage();
      requestMessage.BranchMediumId = branchMediumId; 
      var response = _getSalarySheetYearsRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Years;
    }

    private IEnumerable<BranchMediumAccountsHeadMessageModel> GetBankAccounts(long branchMediumId)
    {
      var currentSession = _currentSessionProvider.GetCurrentSession(branchMediumId);
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetBankAccountAccountHeadsRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      if (currentSession != null) 
        requestMessage.SessionId = currentSession.Id;
      var response = _getBankAccountAccountHeadsRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.BankAccountAccountHeadsList;
    }

    private List<ShowSalaryComponentListResponseMessage.SalaryComponent> GetSalaryComponentList()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSalaryComponentListRequestMessage();
      var response = _showSalaryComponentListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Data.SalaryComponentData
        .OrderByDescending (o=>o.ComponentType.ComponentType)
        .ThenBy(o=>o.ComponentName).ToList();
    }
    
    private List<GetOtherComponentsListResponseMessage.OtherComponent> GetOtherComponentList()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetOtherComponentsListRequestMessage();
      var response = _getOtherComponentsListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.OtherComponents;
    }

    private EmployeeMessageModel GetEmployeeInfo(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowEmployeeRequestMessage();
      requestMessage.EmployeeId = employeeId;
      var response = _showEmployeeRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Employee;
    }

    private GetBankStatementInfoResponseMessage.BankStatementInfo GetBankStatementInfos(long branchMediumId, string bankAccountNumber)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetBankStatementInfoRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      requestMessage.AccountNumber = bankAccountNumber;
      var response = _getBankStatementInfoRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.BankStatementInfos;
    }
  }
}