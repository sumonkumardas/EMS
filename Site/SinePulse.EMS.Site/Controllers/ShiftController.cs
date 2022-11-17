using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ShiftMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Shifts;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class ShiftController : BaseController
  {
    private readonly AddShiftRequestHandler _addShiftRequestHandler;
    private readonly AddShiftResponsePresenter _presenter;
    private readonly ShowShiftListRequestHandler _showShiftListRequestHandler;
    private readonly ShowShiftListResponsePresenter _showShiftListResponsePresenter;
    private readonly EditShiftRequestHandler _editShiftRequestHandler;
    private readonly EditShiftResponsePresenter _editShiftResponsePresenter;
    private readonly ShowShiftRequestHandler _showShiftRequestHandler;
    private readonly ShowShiftResponsePresenter _showShiftResponsePresenter;


    public ShiftController(AddShiftRequestHandler addShiftRequestHandler, AddShiftResponsePresenter presenter,
      ShowShiftListRequestHandler showShiftListRequestHandler, ShowShiftListResponsePresenter showShiftListResponsePresenter,
      EditShiftRequestHandler editShiftRequestHandler, EditShiftResponsePresenter editShiftResponsePresenter,
      ShowShiftRequestHandler showShiftRequestHandler, ShowShiftResponsePresenter showShiftResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addShiftRequestHandler = addShiftRequestHandler;
      _presenter = presenter;
      _showShiftListRequestHandler = showShiftListRequestHandler;
      _showShiftListResponsePresenter = showShiftListResponsePresenter;
      _editShiftRequestHandler = editShiftRequestHandler;
      _editShiftResponsePresenter = editShiftResponsePresenter;
      _showShiftRequestHandler = showShiftRequestHandler;
      _showShiftResponsePresenter = showShiftResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.AddShift)]
    [HttpGet]
    public ActionResult AddShift(ObjectTypeEnum shiftType, long branchId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddShiftViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      ((BaseViewModel)viewModel).LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      
      viewModel.ShiftType = shiftType;
      viewModel.BranchId = branchId;
      
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.AddShift)]
    [HttpPost]
    public ActionResult AddShift(AddShiftViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddShiftRequestMessage();

      requestMessage.ShiftName = model.ShiftName;
      requestMessage.StartTime = model.StartTime;
      requestMessage.EndTime = model.EndTime;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.BranchId = model.BranchId;

      var response = _addShiftRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);

      if (model.ShiftType == ObjectTypeEnum.Branch)
        return RedirectToAction("ViewBranch", "Branch", new {branchId = model.BranchId, activeTab = TabEnum.Shifts});
      
      return RedirectToAction("ViewShift", new {id = response.Result.ShiftId});
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.ViewShift)]
    public ActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowShiftListRequestMessage();
      var model = new ShowShiftListViewModel();

      var response = _showShiftListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showShiftListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.FindShift)]
    public ActionResult ViewShift(long id)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowShiftRequestMessage();
      var model = new ShiftViewModel();
      requestMessage.ShiftId = id;

      var response = _showShiftRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showShiftResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.EditShift)]
    [HttpGet]
    public ActionResult UpdateShift(long id)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowShiftRequestMessage();
      var model = new ShiftViewModel();
      requestMessage.ShiftId = id;

      var response = _showShiftRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showShiftResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.EditShift)]
    [HttpPost]
    public ActionResult UpdateShift(ShiftViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditShiftRequestMessage();

      requestMessage.ShiftId = model.ShiftId;
      requestMessage.ShiftName = model.ShiftName;
      requestMessage.StartTime = model.StartTime;
      requestMessage.EndTime = model.EndTime;
      requestMessage.Status = model.Status;
      requestMessage.BranchId = model.BranchId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editShiftRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editShiftResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      viewModel.BranchId = requestMessage.BranchId;

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);

      return RedirectToAction("ViewBranch", "Branch", new { branchId = requestMessage.BranchId, activeTab = TabEnum.Shifts });
    }
    
    [HttpGet]
    public ActionResult CancelAddShift(ObjectTypeEnum shiftType, long branchId)
    {
      if (shiftType == ObjectTypeEnum.Branch)
        return RedirectToAction("ViewBranch", "Branch", new {branchId = branchId, activeTab = TabEnum.Shifts});
      return RedirectToAction("Index", "Shift");
    }
  }
}