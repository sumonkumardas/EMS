using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.FeatureMessages;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Features;
using SinePulse.EMS.UseCases.Roles;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class RoleController : BaseController
  {
    private readonly DisplayAddRolePageRequestHandler _displayAddRolePageRequestHandler;
    private readonly DisplayAddRolePageResponsePresenter _displayAddRolePageResponsePresenter;
    private readonly AddRoleRequestHandler _addRoleRequestHandler;
    private readonly AddRoleResponsePresenter _presenter;
    private readonly ShowRoleRequestHandler _showRoleRequestHandler;
    private readonly ShowRoleResponsePresenter _showRoleResponsePresenter;
    private readonly ShowRoleListRequestHandler _showRoleListRequestHandler;
    private readonly ShowRoleListResponsePresenter _showRoleListResponsePresenter;
    private readonly GetFeaturesAndAssignedUsersOfRoleRequestHandler _getFeaturesAndAssignedUsersOfRoleRequestHandler;
    private readonly GetFeatureTypeListRequestHandler _getFeatureTypeListRequestHandler;
    private readonly GetFeaturesToAddInRoleRequestHandler _getFeaturesToAddInRoleRequestHandler;
    private readonly AddFeatureInRoleRequestHandler _addFeatureInRoleRequestHandler;
    private readonly AddFeatureInRoleResponsePresenter _addFeatureInRoleResponsePresenter;
    private readonly RemoveFeatureFromRoleRequestHandler _removeFeatureInRoleRequestHandler;
    private readonly RemoveFeatureFromRoleResponsePresenter _removeFeatureInRoleResponsePresenter;
    private readonly GetFeaturesInRoleRequestHandler  _getFeaturesOfRoleRequestHandler;

    public RoleController(
      DisplayAddRolePageRequestHandler displayAddRolePageRequestHandler,
      DisplayAddRolePageResponsePresenter displayAddRolePageResponsePresenter,
      AddRoleRequestHandler addRoleRequestHandler,
      AddRoleResponsePresenter presenter,
      ShowRoleRequestHandler showRoleRequestHandler,
      ShowRoleResponsePresenter showRoleResponsePresenter, 
      ShowRoleListRequestHandler showRoleListRequestHandler,
      ShowRoleListResponsePresenter showRoleListResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler, 
      GetFeaturesAndAssignedUsersOfRoleRequestHandler getFeaturesAndAssignedUsersOfRoleRequestHandler, 
      GetFeatureTypeListRequestHandler getFeatureTypeListRequestHandler, 
      GetFeaturesToAddInRoleRequestHandler getFeaturesToAddInRoleRequestHandler, 
      AddFeatureInRoleRequestHandler addFeatureInRoleRequestHandler, 
      AddFeatureInRoleResponsePresenter addFeatureInRoleResponsePresenter, 
      RemoveFeatureFromRoleRequestHandler removeFeatureInRoleRequestHandler, 
      RemoveFeatureFromRoleResponsePresenter removeFeatureInRoleResponsePresenter, 
      GetFeaturesInRoleRequestHandler getFeaturesOfRoleRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _displayAddRolePageRequestHandler = displayAddRolePageRequestHandler;
      _displayAddRolePageResponsePresenter = displayAddRolePageResponsePresenter;
      _addRoleRequestHandler = addRoleRequestHandler;
      _presenter = presenter;
      _showRoleRequestHandler = showRoleRequestHandler;
      _showRoleResponsePresenter = showRoleResponsePresenter;
      _showRoleListRequestHandler = showRoleListRequestHandler;
      _showRoleListResponsePresenter = showRoleListResponsePresenter;
      _getFeaturesAndAssignedUsersOfRoleRequestHandler = getFeaturesAndAssignedUsersOfRoleRequestHandler;
      _getFeatureTypeListRequestHandler = getFeatureTypeListRequestHandler;
      _getFeaturesToAddInRoleRequestHandler = getFeaturesToAddInRoleRequestHandler;
      _addFeatureInRoleRequestHandler = addFeatureInRoleRequestHandler;
      _addFeatureInRoleResponsePresenter = addFeatureInRoleResponsePresenter;
      _removeFeatureInRoleRequestHandler = removeFeatureInRoleRequestHandler;
      _removeFeatureInRoleResponsePresenter = removeFeatureInRoleResponsePresenter;
      _getFeaturesOfRoleRequestHandler = getFeaturesOfRoleRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.UserManagement, (int)Feature.UserManagementEnum.ShowAllRoles)]
    public IActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowRoleListRequestMessage();
      ShowRoleListViewModel model = new ShowRoleListViewModel();
      
      
      var response = _showRoleListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showRoleListResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());

      
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.UserManagement, (int)Feature.UserManagementEnum.AddRole)]
    [HttpGet]
    public ActionResult AddRole()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddRolePageRequestMessage();

      var model = new AddRoleViewModel();

      var response = _displayAddRolePageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayAddRolePageResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.UserManagement, (int)Feature.UserManagementEnum.AddRole)]
    [HttpPost]
    public ActionResult AddRole(AddRoleViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddRoleRequestMessage();

      requestMessage.RoleName = model.RoleName;
      requestMessage.RoleType = model.RoleType;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addRoleRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }

      return RedirectToAction("Index", "Role");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.UserManagement, (int)Feature.UserManagementEnum.ShowAllRoles)]
    public ActionResult ViewRole(string roleId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowRoleRequestMessage {RoleId = roleId};
      var response = _showRoleRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new RoleViewModel();
      var viewModel = _showRoleResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      var getFeaturesAndUsersRequestMessage = new GetFeaturesAndAssignedUsersOfRoleRequestMessage {RoleId = roleId};
      var featuresAndUsers = _getFeaturesAndAssignedUsersOfRoleRequestHandler.Handle(getFeaturesAndUsersRequestMessage, 
        cancellationToken).Result.FeaturesAndUser;
      viewModel.Features = featuresAndUsers.Features;
      viewModel.RoleUsers = featuresAndUsers.Users;
      return View(viewModel);
    }
    [HttpGet]
    public ActionResult AddFeatureInRole(string roleId)
    {
      var userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddFeatureInRoleViewModel{RoleId = roleId};
      viewModel.FeatureTypes = GetFeatureTypes();
      
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
    public ActionResult AddFeatureInRole(AddFeatureInRoleViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddFeatureInRoleRequestMessage();
      requestMessage.FeatureIds = model.FeatureIds;
      requestMessage.RoleId = model.RoleId;
      var response = _addFeatureInRoleRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addFeatureInRoleResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.RoleId = model.RoleId;
        viewModel.FeatureTypes = GetFeatureTypes();
        return View(viewModel);
      }
      return RedirectToAction("ViewRole", new {roleId = model.RoleId});
    }
    
    [HttpGet]
    public ActionResult RemoveFeatureFromRole(string roleId)
    {
      var userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new RemoveFeatureFromRoleViewModel{RoleId = roleId};
      viewModel.FeatureTypes = GetFeatureTypes();
      
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
    public ActionResult RemoveFeatureFromRole(RemoveFeatureFromRoleViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new RemoveFeatureFromRoleRequestMessage();
      requestMessage.FeatureIds = model.FeatureIds;
      requestMessage.RoleId = model.RoleId;
      var response = _removeFeatureInRoleRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _removeFeatureInRoleResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.RoleId = model.RoleId;
        viewModel.FeatureTypes = GetFeatureTypes();
        return View(viewModel);
      }
      return RedirectToAction("ViewRole", new {roleId = model.RoleId});
    }

    public JsonResult GetFeaturesOfRole(string roleId, long featureTypeId)
    {
      if (roleId != null && featureTypeId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetFeaturesInRoleRequestMessage();
        requestMessage.RoleId = roleId;
        requestMessage.FeatureTypeId = featureTypeId;
        var response = _getFeaturesOfRoleRequestHandler.Handle(requestMessage, cancellationToken);
        if (response.Result.Features != null && response.Result.Features.Any())
        {
          return Json(response.Result.Features);
        }
        return Json(new {noDataFound = "no data Found"});
      }

      return Json(new {noDataFound = "Invalid RoleId or FeatureTypeId..!"});
      
      
    }


    private List<GetFeatureTypeListResponseMessage.FeatureType> GetFeatureTypes()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetFeatureTypeListRequestMessage();
      var response = _getFeatureTypeListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.FeatureTypes;
    }

    public JsonResult GetFeaturesToAddInRole(string roleId, long featureTypeId)
    {
      if (roleId != null && featureTypeId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetFeaturesToAddInRoleRequestMessage();
        requestMessage.RoleId = roleId;
        requestMessage.FeatureTypeId = featureTypeId;
        var response = _getFeaturesToAddInRoleRequestHandler.Handle(requestMessage, cancellationToken);
        if (response.Result.Features != null && response.Result.Features.Any())
        {
          return Json(response.Result.Features);
        }
        return Json(new {noDataFound = "no data Found"});
      }

      return Json(new {noDataFound = "Invalid RoleId or FeatureTypeId..!"});
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.UserManagement, (int)Feature.UserManagementEnum.EditRole)]
    [HttpGet]
    public ActionResult UpdateRole(int? id)
    {
      return View(new AddRoleViewModel());
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.UserManagement, (int)Feature.UserManagementEnum.EditRole)]
    [HttpPost]
    public ActionResult UpdateRole()
    {
      return RedirectToAction("ViewRole", "Role");
    }
  }
}