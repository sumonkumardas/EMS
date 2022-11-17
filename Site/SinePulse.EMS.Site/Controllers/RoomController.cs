using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Rooms;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class RoomController : BaseController
  {
    private readonly AddRoomRequestHandler _addRoomRequestHandler;
    private readonly AddRoomResponsePresenter _presenter;
    private readonly ShowRoomRequestHandler _showRoomRequestHandler;
    private readonly ShowRoomResponsePresenter _showRoomResponsePresenter;
    private readonly EditRoomRequestHandler _editRoomRequestHandler;
    private readonly EditRoomResponsePresenter _editRoomResponsePresenter;
    private readonly ShowRoomsListRequestHandler _showRoomsListRequestHandler;
    private readonly ShowRoomsListResponsePresenter _showRoomsListResponsePresenter;

    public RoomController(AddRoomRequestHandler addRoomRequestHandler, AddRoomResponsePresenter presenter, 
      ShowRoomRequestHandler showRoomRequestHandler, ShowRoomResponsePresenter showRoomResponsePresenter, 
      EditRoomRequestHandler editRoomRequestHandler, EditRoomResponsePresenter editRoomResponsePresenter, 
      ShowRoomsListRequestHandler showRoomsListRequestHandler, ShowRoomsListResponsePresenter showRoomsListResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addRoomRequestHandler = addRoomRequestHandler;
      _presenter = presenter;
      _showRoomRequestHandler = showRoomRequestHandler;
      _showRoomResponsePresenter = showRoomResponsePresenter;
      _editRoomRequestHandler = editRoomRequestHandler;
      _editRoomResponsePresenter = editRoomResponsePresenter;
      _showRoomsListRequestHandler = showRoomsListRequestHandler;
      _showRoomsListResponsePresenter = showRoomsListResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.AddRoom)]
    [HttpGet]
    public ActionResult AddRoom(long branchId, ObjectTypeEnum objectType, long objectId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddRoomViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      ((BaseViewModel)viewModel).LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      viewModel.BranchId = branchId;
      viewModel.ObjectType = objectType;
      viewModel.ObjectId = objectId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.AddRoom)]
    [HttpPost]
    public ActionResult AddRoom(AddRoomViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddRoomRequestMessage();

      requestMessage.RoomNo = model.RoomNo;
      requestMessage.ClassTimeCapacity = model.ClassTimeCapacity;
      requestMessage.ExamTimeCapacity = model.ExamTimeCapacity;
      requestMessage.BranchId = model.BranchId; 
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addRoomRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (model.ObjectType == ObjectTypeEnum.Branch && response.Result.ValidationResult.IsValid)
        return RedirectToAction("ViewBranch","Branch", new {branchId = model.ObjectId, activeTab = TabEnum.Rooms});
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.ViewRoom)]
    public IActionResult Index()
    {
      return View();
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.ViewRoom)]
    public ActionResult ViewRoom(long roomId)
    {
      var cancellationToken = new CancellationToken();
      var model = new RoomViewModel();
      var requestMessage = new ShowRoomRequestMessage
      {
        RoomId = roomId
      };

      var response = _showRoomRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showRoomResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.EditRoom)]
    [HttpGet]
    public ActionResult UpdateRoom(long roomId)
    {
      var cancellationToken = new CancellationToken();
      var model = new RoomViewModel();
      var requestMessage = new ShowRoomRequestMessage
      {
        RoomId = roomId
      };

      var response = _showRoomRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showRoomResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.EditRoom)]
    [HttpPost]
    public ActionResult UpdateRoom(RoomViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditRoomRequestMessage();
      requestMessage.Id = model.Id;
      requestMessage.RoomNo = model.RoomNo;
      requestMessage.ClassTimeCapacity = model.ClassTimeCapacity;
      requestMessage.ExamTimeCapacity = model.ExamTimeCapacity;
      requestMessage.Status = model.Status;
      requestMessage.BranchId = model.Branch.Id;
      
      var response = _editRoomRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editRoomResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if(response.Result.ValidationResult.IsValid)
        return RedirectToAction("ViewBranch", "Branch", new { branchId = model.Branch.Id, activeTab = TabEnum .Rooms});
      
      return View(viewModel);
    }
    
    [HttpGet]
    public ActionResult CancelAddRoom(ObjectTypeEnum objectType, long objectId)
    {
      switch (objectType)
      {
        case ObjectTypeEnum.Branch:
          return RedirectToAction("ViewBranch","Branch",new {branchId = objectId, activeTab = TabEnum.Rooms});
        default:
          return RedirectToAction("Index", "Room");
      }
    }
  }
}