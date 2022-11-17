using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AcademicFeeMessages;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.AcademicFees;
using SinePulse.EMS.UseCases.AccountHeads;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.BranchMediumAccountsHeads;
using SinePulse.EMS.UseCases.Classes;
using SinePulse.EMS.UseCases.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class AcademicFeeController : BaseController
  {
    private readonly ShowClassesListRequestHandler _showClassesListRequestHandler;
    private readonly ShowCurrentSessionListRequestHandler _showSessionListRequestHandler;
    private readonly AddAcademicFeeRequestHandler _addAcademicFeeRequestHandler;
    private readonly AddAcademicFeeResponsePresenter _addAcademicFeeResponsePresenter;
    private readonly GetBranchMediumAccountHeadsRequestHandler _getBranchMediumAccountHeads;
    private readonly GetAcademicFeesRequestHandler _getAcademicFeesRequestHandler;
    private readonly IBranchMediumAccountsHeadPropertyChecker _branchMediumAccountsHeadPropertyChecker;

    public AcademicFeeController(ShowAcademicAccountHeadListRequestHandler showAcademicAccountHeadListRequestHandler,
      ShowClassesListRequestHandler showClassesListRequestHandler,
      ShowCurrentSessionListRequestHandler showSessionListRequestHandler,
      AddAcademicFeeRequestHandler addAcademicFeeRequestHandler,
      AddAcademicFeeResponsePresenter addAcademicFeeResponsePresenter,
      GetBranchMediumAccountHeadsRequestHandler getBranchMediumAccountHeads,
      GetAcademicFeesRequestHandler getAcademicFeesRequestHandler,
      IBranchMediumAccountsHeadPropertyChecker branchMediumAccountsHeadPropertyChecker,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showClassesListRequestHandler = showClassesListRequestHandler;
      _showSessionListRequestHandler = showSessionListRequestHandler;
      _addAcademicFeeRequestHandler = addAcademicFeeRequestHandler;
      _addAcademicFeeResponsePresenter = addAcademicFeeResponsePresenter;
      _getBranchMediumAccountHeads = getBranchMediumAccountHeads;
      _getAcademicFeesRequestHandler = getAcademicFeesRequestHandler;
      _branchMediumAccountsHeadPropertyChecker = branchMediumAccountsHeadPropertyChecker;
    }


    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.SetAcademicFees)]
    [HttpGet]
    public ActionResult AddAcademicFee(long branchMediumId, AcademicFeePeriodEnum feePeriod)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddAcademicFeeViewModel();

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.BranchMediumId = branchMediumId;
      viewModel.Sessions = GetSessions(branchMediumId);
      viewModel.Classes = GetClasses();
      viewModel.AcademicFeePeriod = feePeriod;
      
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      ((BaseViewModel)viewModel).LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      return View(viewModel);
    }
    
    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.SetAcademicFees)]
    [HttpPost]
    public ActionResult AddAcademicFee(AddAcademicFeeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddAcademicFeeRequestMessage();

      requestMessage.FeesCollection = model.FeesCollection;
      requestMessage.BranchMediumId = (long)model.BranchMediumId;
      requestMessage.SessionId = model.SessionId;
      requestMessage.ClassId = model.ClassId;
      requestMessage.AccountHeads = GetBranchMediumAcademicAccountHeads(model.BranchMediumId, model.AcademicFeePeriod);
      requestMessage.AcademicFeePeriod = model.AcademicFeePeriod;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addAcademicFeeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addAcademicFeeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.BranchMediumId = model.BranchMediumId;
        viewModel.AcademicFeePeriod = model.AcademicFeePeriod;
        viewModel.Sessions = GetSessions(model.BranchMediumId);
        viewModel.Classes = GetClasses();
        return View(viewModel);
      }

      return RedirectToAction("ViewBranchMedium", "BranchMedium", new { branchMediumId = model.BranchMediumId });
    }

    private IEnumerable<ClassMessageModel> GetClasses()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowClassesListRequestMessage();
      var response = _showClassesListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Classes;
    }

    private IEnumerable<SessionMessageModel> GetSessions(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowCurrentSessionListRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      var response = _showSessionListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Sessions;
    }

    private IEnumerable<BranchMediumAccountsHeadMessageModel> GetBranchMediumAcademicAccountHeads(
      long branchMediumId, AcademicFeePeriodEnum academicFeePeriodEnum)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetBranchMediumAccountHeadsRequestMessage();
      requestMessage.AccountPeriod = GetAccountPeriodEnum(academicFeePeriodEnum);
      requestMessage.BranchMediumId = branchMediumId;
      var response = _getBranchMediumAccountHeads.Handle(requestMessage, cancellationToken);
      return response.Result.AccountHeads;
    }

    private AccountPeriodEnum GetAccountPeriodEnum(AcademicFeePeriodEnum academicFeePeriod)
    {
      switch (academicFeePeriod)
      {
        case AcademicFeePeriodEnum.Monthly:
          return AccountPeriodEnum.Monthly;
        case AcademicFeePeriodEnum.Yearly:
          return AccountPeriodEnum.Yearly;
        default:
          return AccountPeriodEnum.Monthly;
      }
    }

    [HttpPost]
    public JsonResult GetAcademicFees(long classId, long sessionId, long branchMediumId, AcademicFeePeriodEnum feePeriod)
    {
      if (classId > 0 && sessionId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetAcademicFeesRequestMessage();
        requestMessage.FeePeriod = feePeriod;
        requestMessage.ClassId = classId;
        requestMessage.SessionId = sessionId;
        requestMessage.BranchMediumId = branchMediumId;
        var response = _getAcademicFeesRequestHandler.Handle(requestMessage, cancellationToken);
        if (response.Result.AcademicFees.Any())
          return Json(response.Result.AcademicFees);
        var accountHeads = GetBranchMediumAcademicAccountHeads(branchMediumId, feePeriod);
        if (accountHeads.Any() && IsAlreadyCOAImported(sessionId))
          return Json(accountHeads);
        return Json(new { importCOAValidationMsg = "Import Chart Of Accounts" });
      }
      return Json(new { disableSaveButton = "Disable Save Button" });
    }

    private bool IsAlreadyCOAImported(long sessionId)
    {
      return _branchMediumAccountsHeadPropertyChecker.IsAlreadyCOAImportedInSession(sessionId);
    }
  }
}