using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.AccountHeads;
using SinePulse.EMS.UseCases.Authorization;

namespace SinePulse.EMS.Site.Controllers
{
  
  [Authorize]
  public class AccountHeadController : BaseController
  {
    private readonly ShowAccountHeadListRequestHandler _showAccountHeadListRequestHandler;
    private readonly AddAccountHeadRequestHandler _addAccountHeadRequestHandler;
    private readonly AddAccountHeadResponsePresenter _addAccountHeadResponsePresenter;
    private readonly ShowAccountHeadListResponsePresenter _showAccountHeadListResponsePresenter;
    private readonly ShowAccountHeadRequestHandler _showAccountHeadRequestHandler;
    private readonly ShowAccountHeadResponsePresenter _showAccountHeadPresenter;
    private readonly EditAccountHeadRequestHandler _editAccountHeadRequestHandler;
    private readonly EditAccountHeadResponsePresenter _editAccountHeadResponsePresenter;

    public AccountHeadController(ShowAccountHeadListRequestHandler showAccountHeadListRequestHandler,
      AddAccountHeadRequestHandler addAccountHeadRequestHandler,
      AddAccountHeadResponsePresenter addAccountHeadResponsePresenter, 
      ShowAccountHeadListResponsePresenter showAccountHeadListResponsePresenter, 
      ShowAccountHeadRequestHandler showAccountHeadRequestHandler, 
      ShowAccountHeadResponsePresenter showAccountHeadPresenter, 
      EditAccountHeadRequestHandler editAccountHeadRequestHandler, 
      EditAccountHeadResponsePresenter editAccountHeadResponsePresenter,
       GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showAccountHeadListRequestHandler = showAccountHeadListRequestHandler;
      _addAccountHeadRequestHandler = addAccountHeadRequestHandler;
      _addAccountHeadResponsePresenter = addAccountHeadResponsePresenter;
      _showAccountHeadListResponsePresenter = showAccountHeadListResponsePresenter;
      _showAccountHeadRequestHandler = showAccountHeadRequestHandler;
      _showAccountHeadPresenter = showAccountHeadPresenter;
      _editAccountHeadRequestHandler = editAccountHeadRequestHandler;
      _editAccountHeadResponsePresenter = editAccountHeadResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.COAEntry)]
    [HttpGet]
    public ActionResult AddAccountHead()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddAccountHeadViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        LoginImageUrl = userAssociationMessage.ImageUrl,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,

        AccountHeads = GetAccountHeads()
      };

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.COAEntry)]
    [HttpPost]
    public ActionResult AddAccountHead(AddAccountHeadViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddAccountHeadRequestMessage();

      requestMessage.AccountCode = model.AccountCode;
      requestMessage.AccountHeadName = model.AccountHeadName;
      requestMessage.Status = StatusEnum.Active;
      requestMessage.ParentHeadId = Convert.ToInt64(model.ParentHeadId);
      requestMessage.AccountHeadType = model.AccountHeadType;
      requestMessage.AccountPeriod = model.AccountPeriod;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addAccountHeadRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addAccountHeadResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      viewModel.AccountHeads = GetAccountHeads();
      
      if (response.Result.ValidationResult.IsValid)
        return RedirectToAction("ViewAccountHead",new {accountHeadId = response.Result.AccountHeadId});
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.COAView)]
    public ActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowAccountHeadListRequestMessage();
      AccountHeadListViewModel model = new AccountHeadListViewModel();
      var response = _showAccountHeadListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showAccountHeadListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.COAView)]
    [HttpGet]
    public ActionResult ViewAccountHead(long accountHeadId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowAccountHeadRequestMessage();
      requestMessage.AccountHeadId = accountHeadId;
      var model = new AccountHeadViewModel();
      var response = _showAccountHeadRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showAccountHeadPresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.COAEdit)]
    [HttpGet]
    public ActionResult UpdateAccountHead(long accountHeadId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowAccountHeadRequestMessage();
      requestMessage.AccountHeadId = accountHeadId;
      var model = new AccountHeadViewModel();
      var response = _showAccountHeadRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showAccountHeadPresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      viewModel.AccountHeads = GetAccountHeads();
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Accounting, (int)Feature.AccountingEnum.COAEdit)]
    [HttpPost]
    public ActionResult UpdateAccountHead(AccountHeadViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditAccountHeadRequestMessage();

      requestMessage.AccountHeadId = model.AccountHeadId;
      requestMessage.AccountCode = model.AccountCode;
      requestMessage.AccountHeadName = model.AccountHeadName;
      requestMessage.Status = model.Status;
      requestMessage.ParentHeadId = model.ParentHeadId;
      requestMessage.AccountHeadType = model.AccountHeadType;
      requestMessage.AccountPeriod = model.AccountPeriod;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editAccountHeadRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editAccountHeadResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.AccountHeads = GetAccountHeads();
        return View(viewModel);
      }

      return RedirectToAction("ViewAccountHead", new {accountHeadId = model.AccountHeadId});
    }

    private IEnumerable<AccountHeadMessageModel> GetAccountHeads()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowAccountHeadListRequestMessage();
      var response = _showAccountHeadListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.AccountHeads;
    }
  }
}