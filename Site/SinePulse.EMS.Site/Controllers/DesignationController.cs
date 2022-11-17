using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Designations;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class DesignationController : BaseController
  {
    private readonly AddDesignationRequestHandler _requestHandler;
    private readonly AddDesignationResponsePresenter _presenter;
    private readonly ShowDesignationRequestHandler _showDesignationRequestHandler;
    private readonly ShowDesignationResponsePresenter _showDesignationResponsePresenter;
    private readonly ShowDesignationListRequestHandler _showDesignationListRequestHandler;
    private readonly ShowDesignationListResponsePresenter _showDesignationListResponsePresenter;
    private readonly EditDesignationRequestHandler _editDesignationRequestHandler;
    private readonly EditDesignationResponsePresenter _editDesignationResponsePresenter;

    public DesignationController(AddDesignationRequestHandler requestHandler, AddDesignationResponsePresenter presenter, 
      ShowDesignationRequestHandler showDesignationRequestHandler, 
      ShowDesignationResponsePresenter showDesignationResponsePresenter, 
      ShowDesignationListRequestHandler showDesignationListRequestHandler, 
      ShowDesignationListResponsePresenter showDesignationListResponsePresenter, 
      EditDesignationRequestHandler editDesignationRequestHandler, 
      EditDesignationResponsePresenter editDesignationResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _requestHandler = requestHandler;
      _presenter = presenter;
      _showDesignationRequestHandler = showDesignationRequestHandler;
      _showDesignationResponsePresenter = showDesignationResponsePresenter;
      _showDesignationListRequestHandler = showDesignationListRequestHandler;
      _showDesignationListResponsePresenter = showDesignationListResponsePresenter;
      _editDesignationRequestHandler = editDesignationRequestHandler;
      _editDesignationResponsePresenter = editDesignationResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.ViewDesignation)]
    public IActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowDesignationListRequestMessage();
      DesignationListViewModel model = new DesignationListViewModel();

      var response = _showDesignationListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showDesignationListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.NewDesignation)]
    [HttpGet]
    public ActionResult AddDesignation()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddDesignationViewModel();
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

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.NewDesignation)]
    [HttpPost]
    public ActionResult AddDesignation(AddDesignationViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddDesignationRequestMessage();

      requestMessage.DesignationName = model.DesignationName;
      requestMessage.EmployeeType = model.EmployeeType;
      requestMessage.Status = StatusEnum.Active;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _requestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if(!response.Result.ValidationResult.IsValid)
        return View(viewModel);
      
      return RedirectToAction("Index");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.EditDesignation)]
    [HttpGet]
    public ActionResult UpdateDesignation(long designationId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowDesignationRequestMessage();
      var model = new DesignationViewModel();
      requestMessage.DesignationId = designationId;

      var response = _showDesignationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showDesignationResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.EditDesignation)]
    [HttpPost]
    public ActionResult UpdateDesignation(DesignationViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditDesignationRequestMessage();

      requestMessage.DesignationId = model.Id;
      requestMessage.DesignationName = model.DesignationName;
      requestMessage.EmployeeType = model.EmployeeType;
      requestMessage.Status = model.Status;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editDesignationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editDesignationResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);

      return RedirectToAction("Index");
    }
  }
}