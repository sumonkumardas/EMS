using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.ShiftMessages;
using SinePulse.EMS.Messages.StudentSectionMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Messages.TransactionMessages;
using SinePulse.EMS.Messages.TrialBalanceMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.AccountHeads;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.BalanceSheets;
using SinePulse.EMS.UseCases.Banks;
using SinePulse.EMS.UseCases.Branches;
using SinePulse.EMS.UseCases.BranchMediumAccountsHeads;
using SinePulse.EMS.UseCases.BranchMediums;
using SinePulse.EMS.UseCases.Employee;
using SinePulse.EMS.UseCases.IncomeStatements;
using SinePulse.EMS.UseCases.Mediums;
using SinePulse.EMS.UseCases.Repositories;
using SinePulse.EMS.UseCases.Sections;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.UseCases.Shifts;
using SinePulse.EMS.UseCases.Students;
using SinePulse.EMS.UseCases.StudentSections;
using SinePulse.EMS.UseCases.Terms;
using SinePulse.EMS.UseCases.Transactions;
using SinePulse.EMS.UseCases.TrialBalances;
using SinePulse.EMS.Utility;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class BranchMediumController : BaseController
  {
    private readonly AddBranchMediumRequestHandler _addBranchMediumRequestHandler;
    private readonly AddBranchMediumResponsePresenter _branchResponsePresenter;
    private readonly ShowBranchMediumRequestHandler _showBranchMediumRequestHandler;
    private readonly ShowBranchMediumResponsePresenter _showBranchMediumResponsePresenter;
    private readonly EditBranchMediumRequestHandler _editBranchMediumRequestHandler;
    private readonly EditBranchMediumResponsePresenter _editBranchMediumResponsePresenter;
    private readonly ShowShiftListRequestHandler _showShiftListRequestHandler;
    private readonly ShowMediumListRequestHandler _showMediumListRequestHandler;
    private readonly ShowMediumListResponsePresenter _showMediumListResponsePresenter;
    private readonly ShowShiftListResponsePresenter _showShiftListResponsePresenter;
    private readonly ShowBranchRequestHandler _showBranchRequestHandler;
    private readonly ShowBranchResponsePresenter _showBranchResponsePresenter;
    private readonly ShowAccountHeadListRequestHandler _showAccountHeadListRequestHandler;
    private readonly ImportCOAFromMasterRequestHandler _importCOAFromMasterRequestHandler;
    private readonly IBranchMediumAccountsHeadPropertyChecker _branchMediumAccountsHeadPropertyChecker;
    private readonly ShowTrialBalanceTransactionResponsePresenter _showTrialBalanceTransactionResponsePresenter;
    private readonly ShowTrialBalanceRequestHandler _showTrialBalanceRequestHandler;
    private readonly AccountDisplayResponsePresenter _accountDisplayResponsePresenter;
    private readonly ShowTransactionListRequestHandler _showTransactionListRequestHandler;
    private readonly ShowTransactionListResponsePresenter _showTransactionListResponsePresenter;
    private readonly DisplayBranchMediumRequestHandler _displayBranchMediumRequestHandler;
    private readonly ShowTermListRequestHandler _showTermListRequestHandler;
    private readonly ShowTermListResponsePresenter _showTermListResponsePresenter;
    private readonly ShowEmployeeListRequestHandler _showEmployeeListRequestHandler;
    private readonly ShowEmployeeListResponsePresenter _showEmployeeListResponsePresenter;
    private readonly ShowSessionListRequestHandler _showSessionListRequestHandler;
    private readonly ShowSessionListResponsePresenter _showSessionListResponsePresenter;
    private readonly ShowBankListRequestHandler _showBankListRequestHandler;
    private readonly ShowBankAccountInfoInfoResponsePresenter _showBankAccountInfoInfoResponsePresenter;
    private readonly ShowBranchMediumSectionListRequestHandler _showSectionListRequestHandler;
    private readonly ShowStudentSectionListRequestHandler _showStudentSectionListRequestHandler;
    private readonly ShowStudentSectionListResponsePresenter _showStudentSectionListResponsePresenter;
    private readonly ShowBranchMediumSectionListResponsePresenter _showBranchMediumSectionListResponsePresenter;
    private readonly ShowBranchMediumEmployeeListRequestHandler _showBranchMediumEmployeeListRequestHandler;
    private readonly IWeekDaysConverter _weekDaysConverter;
    public BranchMediumController(AddBranchMediumRequestHandler addBranchMediumRequestHandler,
      AddBranchMediumResponsePresenter branchResponsePresenter,
      EditBranchMediumRequestHandler editBranchMediumRequestHandler,
      EditBranchMediumResponsePresenter editBranchMediumResponsePresenter,
      ShowBranchMediumRequestHandler showBranchMediumRequestHandler,
      ShowBranchMediumResponsePresenter showBranchMediumResponsePresenter,
      ShowMediumListResponsePresenter showMediumListResponsePresenter,
      ShowShiftListResponsePresenter showShiftListResponsePresenter,
      ShowShiftListRequestHandler showShiftListRequestHandler,
      ShowMediumListRequestHandler showMediumListRequestHandler,
      ShowBranchRequestHandler showBranchRequestHandler, 
      ShowBranchResponsePresenter showBranchResponsePresenter,
      ShowAccountHeadListRequestHandler showAccountHeadListRequestHandler,
      ImportCOAFromMasterRequestHandler importCoaFromMasterRequestHandler,
      ShowBranchMediumAccountsHeadListRequestHandler showBranchMediumAccountsHeadListRequestHandler,
      IBranchMediumAccountsHeadPropertyChecker branchMediumAccountsHeadPropertyChecker,
      ShowTrialBalanceTransactionResponsePresenter showTrialBalanceTransactionResponsePresenter,
      ShowIncomeStatementTransactionResponsePresenter showIncomeStatementTransactionResponsePresenter,
      ShowTrialBalanceRequestHandler showTrialBalanceRequestHandler,
      ShowIncomeStatementRequestHandler showIncomeStatementRequestHandler,
      AccountDisplayResponsePresenter accountDisplayResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      ShowBalanceSheetRequestHandler showBalanceSheetRequestHandler,
      ShowBalanceSheetTransactionResponsePresenter showBalanceSheetTransactionResponsePresenter,
      ShowBranchMediumAccountsHeadListResponsePresenter showBranchMediumAccountsHeadListResponsePresenter,
      ShowTransactionListRequestHandler showTransactionListRequestHandler,
      ShowTransactionListResponsePresenter showTransactionListResponsePresenter,
      DisplayBranchMediumRequestHandler displayBranchMediumRequestHandler,
      DisplayBranchMediumResponsePresenter displayBranchMediumResponsePresenter,
      ICurrentSessionProvider currentSessionProvider, 
      ShowTermListRequestHandler showTermListRequestHandler,
      ShowTermListResponsePresenter showTermListResponsePresenter,
      ShowStudentListRequestHandler showStudentListRequestHandler,
      ShowStudentListResponsePresenter showStudentListResponsePresenter,
      ShowEmployeeListRequestHandler showEmployeeListRequestHandler,
      ShowEmployeeListResponsePresenter showEmployeeListResponsePresenter,
      ShowSessionListRequestHandler showSessionListRequestHandler,
      ShowSessionListResponsePresenter showSessionListResponsePresenter,
      ShowBankListRequestHandler showBankListRequestHandler, 
      ShowBankAccountInfoInfoResponsePresenter showBankAccountInfoInfoResponsePresenter,
      ShowBranchMediumSectionListRequestHandler showSectionListRequestHandler,
      ShowStudentSectionListRequestHandler showStudentSectionListRequestHandler,
      ShowStudentSectionListResponsePresenter showStudentSectionListResponsePresenter,
      ShowBranchMediumSectionListResponsePresenter showBranchMediumSectionListResponsePresenter,
      ShowBranchMediumEmployeeListRequestHandler showBranchMediumEmployeeListRequestHandler,
      IWeekDaysConverter weekDaysConverter) : base(getUserAssociationRequestHandler)
    {
      _addBranchMediumRequestHandler = addBranchMediumRequestHandler;
      _branchResponsePresenter = branchResponsePresenter;
      _editBranchMediumRequestHandler = editBranchMediumRequestHandler;
      _editBranchMediumResponsePresenter = editBranchMediumResponsePresenter;
      _showBranchMediumRequestHandler = showBranchMediumRequestHandler;
      _showBranchMediumResponsePresenter = showBranchMediumResponsePresenter;
      _showShiftListRequestHandler = showShiftListRequestHandler;
      _showMediumListRequestHandler = showMediumListRequestHandler;
      _showMediumListResponsePresenter = showMediumListResponsePresenter;
      _showShiftListResponsePresenter = showShiftListResponsePresenter;
      _showBranchRequestHandler = showBranchRequestHandler;
      _showBranchResponsePresenter = showBranchResponsePresenter;
      _showAccountHeadListRequestHandler = showAccountHeadListRequestHandler;
      _importCOAFromMasterRequestHandler = importCoaFromMasterRequestHandler;
      _branchMediumAccountsHeadPropertyChecker = branchMediumAccountsHeadPropertyChecker;
      _showTrialBalanceTransactionResponsePresenter = showTrialBalanceTransactionResponsePresenter;
      _showTrialBalanceRequestHandler = showTrialBalanceRequestHandler;
      _accountDisplayResponsePresenter = accountDisplayResponsePresenter;
      _showTransactionListRequestHandler = showTransactionListRequestHandler;
      _showTransactionListResponsePresenter = showTransactionListResponsePresenter;
      _displayBranchMediumRequestHandler = displayBranchMediumRequestHandler;
      _showTermListRequestHandler = showTermListRequestHandler;
      _showTermListResponsePresenter = showTermListResponsePresenter;
      _showEmployeeListRequestHandler = showEmployeeListRequestHandler;
      _showEmployeeListResponsePresenter = showEmployeeListResponsePresenter;
      _showSessionListRequestHandler = showSessionListRequestHandler;
      _showSessionListResponsePresenter = showSessionListResponsePresenter;
      _showBankListRequestHandler = showBankListRequestHandler;
      _showBankAccountInfoInfoResponsePresenter = showBankAccountInfoInfoResponsePresenter;
      _showSectionListRequestHandler = showSectionListRequestHandler;
      _showStudentSectionListRequestHandler = showStudentSectionListRequestHandler;
      _showStudentSectionListResponsePresenter = showStudentSectionListResponsePresenter;
      _showBranchMediumSectionListResponsePresenter = showBranchMediumSectionListResponsePresenter;
      _showBranchMediumEmployeeListRequestHandler = showBranchMediumEmployeeListRequestHandler;
      _weekDaysConverter = weekDaysConverter;
    }


    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.AddBranchMedium)]
    [HttpGet]
    public ActionResult AddBranchMedium(long branchId)
    {
      var cancellationToken = new CancellationToken();

      var showBranchRequestMessage = new ShowBranchRequestMessage();
      showBranchRequestMessage.BranchId = branchId;
      ShowBranchViewModel showBranchViewModel = new ShowBranchViewModel();
      var showBranchResponse = _showBranchRequestHandler.Handle(showBranchRequestMessage, cancellationToken);
      var pickedBranchViewModel = _showBranchResponsePresenter.Handle(showBranchResponse.Result, showBranchViewModel, GetUserAssociationResponseMessage());

      var showShiftListRequestMessage = new ShowShiftListRequestMessage();
      showShiftListRequestMessage.BranchId = branchId;
      ShowShiftListViewModel showShiftListViewModel = new ShowShiftListViewModel();
      var showShiftListResponse = _showShiftListRequestHandler.Handle(showShiftListRequestMessage, cancellationToken);
      var pickedShiftListViewModel = _showShiftListResponsePresenter.Handle(showShiftListResponse.Result, showShiftListViewModel, GetUserAssociationResponseMessage());

      var showMediumListRequestMessage = new ShowMediumListRequestMessage();
      ShowMediumListViewModel showMediumListViewModel = new ShowMediumListViewModel();
      var showMediumListResponse = _showMediumListRequestHandler.Handle(showMediumListRequestMessage, cancellationToken);
      var pickedMediumListViewModel = _showMediumListResponsePresenter.Handle(showMediumListResponse.Result, showMediumListViewModel, GetUserAssociationResponseMessage());

      var model = new AddBranchMediumViewModel();
      model.LoginName = pickedBranchViewModel.LoginName;
      model.AssociatedWith = pickedBranchViewModel.AssociatedWith;
      model.LoginUsersInstituteId = pickedBranchViewModel.LoginUsersInstituteId;
      model.LoginUsersBranchMediumId = pickedBranchViewModel.LoginUsersBranchMediumId;
      model.RoleFeatures = pickedBranchViewModel.RoleFeatures;
      model.LoginUsersBranchId = branchId;
      model.Shifts = pickedShiftListViewModel.Shifts;
      model.Mediums = pickedMediumListViewModel.Mediums;
      model.BranchName = pickedBranchViewModel.Branch.BranchName;
      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.AddBranchMedium)]
    [HttpPost]
    public ActionResult AddBranchMedium(AddBranchMediumViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddBranchMediumRequestMessage();

      requestMessage.WeeklyHolidaysList = model.WeeklyHolidaysList;
      requestMessage.BranchId = model.BranchId;
      requestMessage.MediumId = Convert.ToInt64(model.MediumId);
      requestMessage.ShiftId = Convert.ToInt64(model.ShiftId);
      requestMessage.SessionBufferPeriod = model.SessionBufferPeriod;

      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _branchResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.BranchMediumId > 0)
      {
        return RedirectToAction("ViewBranchMedium", new { branchMediumId = response.Result.BranchMediumId });
      }
      else
      {
        var showBranchRequestMessage = new ShowBranchRequestMessage();
        showBranchRequestMessage.BranchId = (long)model.BranchId;
        ShowBranchViewModel showBranchViewModel = new ShowBranchViewModel();
        var showBranchResponse = _showBranchRequestHandler.Handle(showBranchRequestMessage, cancellationToken);
        var pickedBranchViewModel = _showBranchResponsePresenter.Handle(showBranchResponse.Result, showBranchViewModel, GetUserAssociationResponseMessage());

        var showShiftListRequestMessage = new ShowShiftListRequestMessage();
        showShiftListRequestMessage.BranchId = (long)model.BranchId;
        ShowShiftListViewModel showShiftListViewModel = new ShowShiftListViewModel();
        var showShiftListResponse = _showShiftListRequestHandler.Handle(showShiftListRequestMessage, cancellationToken);
        var pickedShiftListViewModel = _showShiftListResponsePresenter.Handle(showShiftListResponse.Result, showShiftListViewModel, GetUserAssociationResponseMessage());

        var showMediumListRequestMessage = new ShowMediumListRequestMessage();
        ShowMediumListViewModel showMediumListViewModel = new ShowMediumListViewModel();
        var showMediumListResponse = _showMediumListRequestHandler.Handle(showMediumListRequestMessage, cancellationToken);
        var pickedMediumListViewModel = _showMediumListResponsePresenter.Handle(showMediumListResponse.Result, showMediumListViewModel, GetUserAssociationResponseMessage());

        viewModel.Shifts = pickedShiftListViewModel.Shifts;
        viewModel.Mediums = pickedMediumListViewModel.Mediums;
        viewModel.BranchName = pickedBranchViewModel.Branch.BranchName;
        return View(viewModel);
      }
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.EditBranchMedium)]
    public ActionResult UpdateBranchMedium(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchMediumRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;

      ShowBranchMediumViewModel model = new ShowBranchMediumViewModel();
      var response = _showBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchMediumResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      var showBranchRequestMessage = new ShowBranchRequestMessage();
      showBranchRequestMessage.BranchId = viewModel.BranchMedium.Branch.Id;
      ShowBranchViewModel showBranchViewModel = new ShowBranchViewModel();
      var showBranchResponse = _showBranchRequestHandler.Handle(showBranchRequestMessage, cancellationToken);
      var pickedBranchViewModel = _showBranchResponsePresenter.Handle(showBranchResponse.Result, showBranchViewModel, GetUserAssociationResponseMessage());

      var editViewModel = new EditBranchMediumViewModel();
      editViewModel.LoginName = viewModel.LoginName;
      editViewModel.AssociatedWith = viewModel.AssociatedWith;
      editViewModel.LoginUsersInstituteId = viewModel.LoginUsersInstituteId;
      editViewModel.LoginUsersBranchMediumId = viewModel.LoginUsersBranchMediumId;
      editViewModel.RoleFeatures = viewModel.RoleFeatures;
      editViewModel.LoginUsersBranchId = viewModel.BranchMedium.Branch.Id;
      editViewModel.WeeklyHolidaysList = _weekDaysConverter.ConvertToDaysOfWeekList(viewModel.BranchMedium.WeeklyHolidays);
      editViewModel.LoginUsersBranchMediumId = viewModel.BranchMedium.Id;
      editViewModel.MediumId = viewModel.BranchMedium.Medium.Id;
      editViewModel.ShiftId = viewModel.BranchMedium.Shift.Id;
      editViewModel.BranchName = pickedBranchViewModel.Branch.BranchName;
      editViewModel.Mediums = GetMediums();
      editViewModel.Shifts = GetShifts(viewModel.BranchMedium.Branch.Id);
      editViewModel.BranchMediumId = branchMediumId;
      editViewModel.BranchId = viewModel.BranchMedium.Branch.Id;
      editViewModel.SessionBufferPeriod = viewModel.BranchMedium.SessionBufferPeriods;
      return View(editViewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.EditBranchMedium)]
    [HttpPost]
    public ActionResult UpdateBranchMedium(EditBranchMediumViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditBranchMediumRequestMessage();

      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.WeeklyHolidaysList = model.WeeklyHolidaysList;
      requestMessage.BranchId = model.BranchId;
      requestMessage.MediumId = model.MediumId;
      requestMessage.ShiftId = model.ShiftId;
      requestMessage.SessionBufferPeriod = model.SessionBufferPeriod;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editBranchMediumResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (response.Result.ValidationResult.IsValid)
      {
        return RedirectToAction("ViewBranch", "Branch", new { branchId = model.BranchId, activeTab = TabEnum.Mediums });
      }
      viewModel.BranchMediumId = model.BranchMediumId;
      viewModel.BranchId = model.BranchId;
      viewModel.MediumId = model.MediumId;
      viewModel.Mediums = GetMediums();
      viewModel.Shifts = GetShifts(model.BranchId);
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.ViewBranchMedium)]
    public ActionResult ViewBranchMedium(long branchMediumId, TabEnum activeTab)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchMediumRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      ShowBranchMediumViewModel model = new ShowBranchMediumViewModel();
      var response = _showBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchMediumResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      viewModel.InstituteSessions = response.Result.InstituteSessions;
      viewModel.Institute = response.Result.Institute;
      viewModel.ActiveTab = activeTab;
      
      return View(viewModel);
    }

    public ActionResult GetExamTerms(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var termListRequestMessage = new ShowTermListRequestMessage
      {
        Year = DateTime.Now.Year, BranchMediumId = branchMediumId
      };
      var response = _showTermListRequestHandler.Handle(termListRequestMessage, cancellationToken);
      var viewModel = _showTermListResponsePresenter.Handle(response.Result.Data, new ShowTermListViewModel(), 
        GetUserAssociationResponseMessage());
      return PartialView("ShowTermList", viewModel);
    }

    public IEnumerable<ShowEmployeeListResponseMessage.Employee> GetEmployees(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();

      var showEmployeeListRequestMessage = new ShowEmployeeListRequestMessage
      {
        ObjectId = branchMediumId,
        AssociationType = AssociationType.BranchMedium
      };
      var response = _showEmployeeListRequestHandler.Handle(showEmployeeListRequestMessage, cancellationToken);
      var viewModel = _showEmployeeListResponsePresenter.Handle(response.Result, new EmployeeListViewModel(), GetUserAssociationResponseMessage());

      return (viewModel.EmployeeList!= null && viewModel.EmployeeList.Count > 0) ? viewModel.EmployeeList : new List<ShowEmployeeListResponseMessage.Employee>();

    }

    public IEnumerable<Section> GetSections(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchMediumRequestMessage();

      requestMessage.BranchMediumId = branchMediumId;

      var response = _showBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      if (response.Result.BranchMedium != null)
        return response.Result.BranchMedium.Sections;
      else
        return new List<Section>();

    }

    public JsonResult ImportCOAFromMaster(long branchMediumId, long sessionId)
    {
      var responseMessage = new { msg = "Import Charts Of Accounts from Master Successful." };
      if (sessionId > 0 && !IsAlreadyCOAImported(sessionId))
      {
        var cancellationToken = new CancellationToken();
        var accountHeadListRequestMessageRequestMessage = new ShowAccountHeadListRequestMessage();
        var accountHeads = _showAccountHeadListRequestHandler.Handle(accountHeadListRequestMessageRequestMessage, cancellationToken).Result.AccountHeads;
        var requestMessage = new ImportCOAFromMasterRequestMessage();
        requestMessage.BranchMediumId = branchMediumId;
        requestMessage.SessionId = sessionId;
        requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
        requestMessage.AccountsHeads = accountHeads;
        var response = _importCOAFromMasterRequestHandler.Handle(requestMessage, cancellationToken);
      }
      else if (IsAlreadyCOAImported(sessionId))
      {
        responseMessage = new { msg = "Charts Of Accounts Already Added in Session." };
      }
      else
      {
        responseMessage = new { msg = "Current Session Not Found!" };
      }

      return Json(responseMessage);
    }

    private bool IsAlreadyCOAImported(long sessionId)
    {
      return _branchMediumAccountsHeadPropertyChecker.IsAlreadyCOAImportedInSession(sessionId);
    }

    private long GetCurrentSession(ICollection<Session> branchMediumSessions,
      ICollection<Session> branchSessions, ICollection<SessionMessageModel> resultInstituteSessions)
    {
      if (branchMediumSessions != null && branchMediumSessions.Any())
      {
        var session = branchMediumSessions.FirstOrDefault(s => s.StartDate <= DateTime.Now && DateTime.Now <= s.EndDate);
        return session?.Id ?? 0;
      }
      if (branchSessions != null && branchSessions.Any())
      {
        var session = branchSessions.FirstOrDefault(s => s.StartDate <= DateTime.Now && DateTime.Now <= s.EndDate);
        return session?.Id ?? 0;
      }
      if (resultInstituteSessions != null && resultInstituteSessions.Any())
      {
        var session = resultInstituteSessions.FirstOrDefault(s => s.StartDate <= DateTime.Now && DateTime.Now <= s.EndDate);
        return session?.Id ?? 0;
      }
      return 0;
    }

    private Session GetCurrentSessionObject(ICollection<Session> branchMediumSessions,
        ICollection<Session> branchSessions, ICollection<SessionMessageModel> resultInstituteSessions)
    {
      if (branchMediumSessions != null && branchMediumSessions.Any())
      {
        var session = branchMediumSessions.FirstOrDefault(s => s.StartDate <= DateTime.Now && DateTime.Now <= s.EndDate);
        return session;
      }
      if (branchSessions != null && branchSessions.Any())
      {
        var session = branchSessions.FirstOrDefault(s => s.StartDate <= DateTime.Now && DateTime.Now <= s.EndDate);
        return session;
      }
      if (resultInstituteSessions != null && resultInstituteSessions.Any())
      {
        var session = resultInstituteSessions.FirstOrDefault(s => s.StartDate <= DateTime.Now && DateTime.Now <= s.EndDate);
        if (session != null)
          return new Session() { Id = session.Id, StartDate = session.StartDate, EndDate = session.EndDate };
      }
      return null;
    }
    
    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.AccountView)]
    public ActionResult ViewAccount(long branchMediumId, TabEnum activeTab)
    {
      var cancellationToken = new CancellationToken();
      AccountDisplayModel viewModel = new AccountDisplayModel();
      var requestMessage = new DisplayBranchMediumRequestMessage { BranchMediumId = branchMediumId,LoadSessions = false};
      var response = _displayBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      viewModel = _accountDisplayResponsePresenter.Handle(response.Result, viewModel, GetUserAssociationResponseMessage());
      viewModel.Transactions = GetTransactions(branchMediumId);
      
      return View(viewModel);
    }

    private List<TransactionViewModel> GetTransactions(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowTransactionListRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      requestMessage.StartDate = DateTime.Now;
      requestMessage.EndDate = DateTime.Now;

      var response = _showTransactionListRequestHandler.Handle(requestMessage, cancellationToken);
      return _showTransactionListResponsePresenter.Handle(response.Result, new List<TransactionViewModel>());
    }

    

    public ActionResult GetStudents(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      //var showStudentListRequestMessage = new ShowStudentListRequestMessage
      //{
      //  BranchMediumId = branchMediumId
      //};
      //var response = _showStudentListRequestHandler.Handle(showStudentListRequestMessage, cancellationToken);
      //var viewModel = _showStudentListResponsePresenter.Handle(response.Result,new ShowStudentListViewModel(), GetUserAssociationResponseMessage());
      var sessionListRequestMessage = new ShowSessionListRequestMessage
      {
        BranchMediumId = branchMediumId
      };
      var sessionListResponseMessage =
        _showSessionListRequestHandler.Handle(sessionListRequestMessage, cancellationToken);
      var model =
        _showSessionListResponsePresenter.Handle(sessionListResponseMessage.Result, new ShowSessionListViewModel(), GetUserAssociationResponseMessage());

      var viewModel = new ShowStudentListViewModel();
      viewModel.BranchMediumId = branchMediumId;
      viewModel.Sessions = model.Sessions.ToList();
      return PartialView("ShowStudentList", viewModel);
    }

    public ActionResult GetEmployeeDatas(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var showBranchMediumEmployeeListRequestMessage = new ShowBranchMediumEmployeeListRequestMessage
      {
        BranchMediumId = branchMediumId,
        AssociationType = AssociationType.BranchMedium
      };
      var response = _showBranchMediumEmployeeListRequestHandler.Handle(showBranchMediumEmployeeListRequestMessage, cancellationToken);
      var viewModel = _showEmployeeListResponsePresenter.Handle(response.Result, new EmployeeListViewModel(), GetUserAssociationResponseMessage());
      viewModel.BranchMediumId = branchMediumId;
      return PartialView("ShowEmployeeList", viewModel);
    }

    public ActionResult GetSessions(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var sessionListRequestMessage = new ShowSessionListRequestMessage
      {
        BranchMediumId = branchMediumId
      };
      var sessionListResponseMessage =
        _showSessionListRequestHandler.Handle(sessionListRequestMessage, cancellationToken);
      var viewModel =
        _showSessionListResponsePresenter.Handle(sessionListResponseMessage.Result, new ShowSessionListViewModel(), GetUserAssociationResponseMessage());

      return PartialView("ShowSessionList", viewModel);
    }

    public ActionResult GetTrialBalance(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchMediumRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      ShowBranchMediumViewModel model = new ShowBranchMediumViewModel();
      var response = _showBranchMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchMediumResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      viewModel.InstituteSessions = response.Result.InstituteSessions;
      viewModel.Institute = response.Result.Institute;
      var currentSession = response.Result.CurrentSession;

      var trialBalanceViewModel = new TrialBalanceViewModel { StartDate = DateTime.Now, EndDate = DateTime.Now };
      if (currentSession != null)
      {
        viewModel.CurrentSessionId = currentSession.Id;

        var startDate = (currentSession != null) ? currentSession.StartDate : DateTime.Now;
        var endDate = DateTime.Now;


        var showTrialBalanceRequestMessage = new ShowTrialBalanceRequestMessage
        {
          BranchMediumId = branchMediumId,
          StartDate = startDate,
          EndDate = endDate,
          CurrentUserName = HttpContext.User.Identity.Name
        };

        var showTrialBalanceResponse =
            _showTrialBalanceRequestHandler.Handle(showTrialBalanceRequestMessage, cancellationToken);
        trialBalanceViewModel = new TrialBalanceViewModel { StartDate = startDate, EndDate = endDate };
        if (showTrialBalanceResponse.Result.Data != null)
        {
          trialBalanceViewModel =
              _showTrialBalanceTransactionResponsePresenter.Handle(showTrialBalanceResponse.Result.Data,
                  trialBalanceViewModel);
          TrialBalanceViewModel.BranchMedium trialBalanceBranchMedium = new TrialBalanceViewModel.BranchMedium()
          {
            BranchMediumId = viewModel.BranchMedium.Id,
            BranchMediumName = "<p>" + viewModel.Institute.OrganisationName + "</p><p>" + viewModel.BranchMedium.Branch.BranchName + "</p><p>" + viewModel.BranchMedium.Medium.MediumName + "</p>"
          };

          trialBalanceViewModel.Branch = trialBalanceBranchMedium;
        }
        else
        {
          trialBalanceViewModel = null;
        }
        
      }
      else
      {
        return new EmptyResult();
      }
      if (trialBalanceViewModel == null)
        return new EmptyResult();
      return PartialView("~/Views/Accounting/ShowTrialBalanceWithoutFiltering.cshtml", trialBalanceViewModel);
    }

    public ActionResult GetBankInfo(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var showBankInfoListRequestMessage = new ShowBankListRequestMessage()
      {
        BranchMediumId = branchMediumId
      };
      var response =
        _showBankListRequestHandler.Handle(showBankInfoListRequestMessage, cancellationToken);
      var viewModel =
        _showBankAccountInfoInfoResponsePresenter.Handle(response.Result, new ShowBankAccountInfoListViewModel(), GetUserAssociationResponseMessage());

      return PartialView("ShowBankInfoList", viewModel);
    }

    public IEnumerable<ClassMessageModel> GetClassOfBranchMedium(long branchMediumId,long sessionId)
    {
      var cancellationToken = new CancellationToken();
      var branchMediumRequestMessage = new ShowBranchMediumRequestMessage { BranchMediumId = branchMediumId };
      var branchMedium = _showBranchMediumRequestHandler.Handle(branchMediumRequestMessage, cancellationToken).Result.BranchMedium;
      var sectionListRequestMessage = new ShowBranchMediumSectionListRequestMessage
      {
        BranchId = branchMedium.Branch.Id,
        MediumId = branchMedium.Medium.Id
      };
      return _showSectionListRequestHandler.Handle(sectionListRequestMessage, cancellationToken).Result.Sections.Where(s=>s.Session.Id == sessionId).Select(x=>x.Class);
    }

    public IEnumerable<SectionMessageModel> GetSectionOfBranchMedium(long branchMediumId, long sessionId,long classId)
    {
      var cancellationToken = new CancellationToken();
      var branchMediumRequestMessage = new ShowBranchMediumRequestMessage { BranchMediumId = branchMediumId };
      var branchMedium = _showBranchMediumRequestHandler.Handle(branchMediumRequestMessage, cancellationToken).Result.BranchMedium;
      var sectionListRequestMessage = new ShowBranchMediumSectionListRequestMessage
      {
        BranchId = branchMedium.Branch.Id,
        MediumId = branchMedium.Medium.Id
      };
      return _showSectionListRequestHandler.Handle(sectionListRequestMessage, cancellationToken).Result.Sections.Where(s => s.Session.Id == sessionId && s.Class.Id == classId);
    }

    public ActionResult GetStudentSectionOfBranchMedium(long branchMediumId, long sessionId, long classId,long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var showStudentSectionListRequestMessage = new ShowStudentSectionListRequestMessage() { ClassId = classId,SectionId = sectionId};

      var response = _showStudentSectionListRequestHandler.Handle(showStudentSectionListRequestMessage, cancellationToken);
      var model = _showStudentSectionListResponsePresenter
        .Handle(response.Result, new ShowStudentListViewModel(), GetUserAssociationResponseMessage());
      model.BranchMediumId = branchMediumId;
      return PartialView("ShowStudentListTable", model);
    }

    public ActionResult GetFilteredSectionBySession(long branchMediumId, long sessionId)
    {
      var cancellationToken = new CancellationToken();
      var branchMediumRequestMessage = new ShowBranchMediumRequestMessage { BranchMediumId = branchMediumId };
      var branchMedium = _showBranchMediumRequestHandler.Handle(branchMediumRequestMessage, cancellationToken).Result.BranchMedium;
      var sectionListRequestMessage = new ShowBranchMediumSectionListRequestMessage
      {
        BranchId = branchMedium.Branch.Id,
        MediumId = branchMedium.Medium.Id
      };
      var responseModel = _showSectionListRequestHandler.Handle(sectionListRequestMessage, cancellationToken).Result.Sections.Where(s => s.Session.Id == sessionId);
      var model = _showBranchMediumSectionListResponsePresenter.Handle(responseModel.ToList(),
        new ShowBranchMediumSectionListViewModel(), GetUserAssociationResponseMessage());

      return PartialView("ShowClassListTableFiltered", model);
    }

    public ActionResult GetExamTermsFiltered(long branchMediumId,long sessionId)
    {
      var cancellationToken = new CancellationToken();
      var termListRequestMessage = new ShowTermListRequestMessage
      {
        Year = DateTime.Now.Year,
        BranchMediumId = branchMediumId,
        SessionId = sessionId
      };
      var response = _showTermListRequestHandler.Handle(termListRequestMessage, cancellationToken);
      var viewModel = _showTermListResponsePresenter.Handle(response.Result.Data, new ShowTermListViewModel(),
        GetUserAssociationResponseMessage());
      viewModel.Terms = viewModel.Terms.Where(x => x.SessionData.SessionId == sessionId).ToList();
      return PartialView("ShowTermList", viewModel);
    }
    
    private IEnumerable<ShiftMessageModel> GetShifts(long branchId)
    {
      var cancellationToken = new CancellationToken();
      var showShiftListRequestMessage = new ShowShiftListRequestMessage();
      showShiftListRequestMessage.BranchId = branchId;
      ShowShiftListViewModel showShiftListViewModel = new ShowShiftListViewModel();
      var showShiftListResponse = _showShiftListRequestHandler.Handle(showShiftListRequestMessage, cancellationToken);
      var pickedShiftListViewModel = _showShiftListResponsePresenter.Handle(showShiftListResponse.Result, 
        showShiftListViewModel, GetUserAssociationResponseMessage());

      return pickedShiftListViewModel.Shifts;
    }

    private IEnumerable<MediumMessageModel> GetMediums()
    {
      var cancellationToken = new CancellationToken();
      var showMediumListRequestMessage = new ShowMediumListRequestMessage();
      ShowMediumListViewModel showMediumListViewModel = new ShowMediumListViewModel();
      var showMediumListResponse = _showMediumListRequestHandler.Handle(showMediumListRequestMessage, cancellationToken);
      var pickedMediumListViewModel = _showMediumListResponsePresenter.Handle(showMediumListResponse.Result, 
        showMediumListViewModel, GetUserAssociationResponseMessage());
      return pickedMediumListViewModel.Mediums;
    }
  }
}